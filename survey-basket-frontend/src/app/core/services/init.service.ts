import { inject, Injectable } from '@angular/core';
import { TokenService } from './token.service';
import { Router } from '@angular/router';
import { AuthService } from './auth.service';
import { tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class InitService {

  authService = inject(AuthService);
  router = inject(Router);
  private initialized = false;

  init() {
    return this.authService.checkInitialAuth().pipe(
      tap(() => this.initialized = true)
    );
  }

  isReady() {
    return this.initialized;
  }
}
