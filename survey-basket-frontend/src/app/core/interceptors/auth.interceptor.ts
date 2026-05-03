import {
  HttpInterceptorFn,
  HttpErrorResponse,
  HttpRequest,
  HttpHandlerFn,
} from '@angular/common/http';
import { inject } from '@angular/core';
import { catchError, switchMap, throwError, BehaviorSubject, filter, take } from 'rxjs';
import { TokenService } from '../services/token.service';
import { AuthService } from '../services/auth.service';
import { Router } from '@angular/router';
import { MatSnackBar } from '@angular/material/snack-bar';
import { InitService } from '../services/init.service';


function addBearerToken(req: HttpRequest<unknown>, token: string | null): HttpRequest<unknown> {
  if (!token) return req;
  return req.clone({ setHeaders: { Authorization: `Bearer ${token}` } });
}

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService);
  //const authService = inject(AuthService);
  const router = inject(Router);

  const token = tokenService.accessToken();
  const initService = inject(InitService);

  if (!initService.isReady()) {
    return next(req); // skip auth until ready
  }
  const clonedRequest = token ? req.clone({ setHeaders: { Authorization: `Bearer ${token}` } }) : req;

  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {
      if (shouldHandle401(error, req)) {
        inject(MatSnackBar).open("Not Authenticated User");
        router.navigateByUrl('/auth/login')
      }
      return throwError(() => error);
    })
  );
};

function shouldHandle401(error: HttpErrorResponse, req: HttpRequest<unknown>): boolean {
  return (
    error.status === 401 &&
    !req.url.includes('Auth/Login') &&
    !req.url.includes('Auth/RefreshAuth')
  );
}

