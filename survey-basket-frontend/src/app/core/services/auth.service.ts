import { Injectable, inject, computed } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { catchError, map, tap } from 'rxjs/operators';
import { Observable, of } from 'rxjs';
import { environment } from '../../../environments/environment';
import { TokenService } from './token.service';
import { AuthResponse } from "../../shared/models/Auth/AuthResponse";
import { ResetPasswordRequest } from "../../shared/models/Account/ResetPasswordRequest";
import { ResendConfirmationRequest } from "../../shared/models/Account/ResendConfirmationRequest";
import { EmailConfirmRequest } from "../../shared/models/Account/EmailConfirmRequest";
import { RegisterRequest } from "../../shared/models/Account/RegisterRequest";
import { RefreshTokenRequest } from "../../shared/models/Auth/RefreshTokenRequest";
import { AuthRequest } from "../../shared/models/Auth/AuthRequest";

@Injectable({ providedIn: 'root' })
export class AuthService {
  refreshIfNeeded() {
    throw new Error('Method not implemented.');
  }
  private readonly http = inject(HttpClient);
  private readonly tokenService = inject(TokenService);
  private readonly router = inject(Router);
  private readonly base = `${environment.apiUrl}/api/Auth`;

  readonly isLoggedIn = this.tokenService.isLoggedIn;
  readonly currentUserRoles = computed(() => this.tokenService.getUserRoles());

  login(credentials: AuthRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.base}/Login`, credentials).pipe(
      tap((response) => {
        this.tokenService.setTokens(response)

      })
    );
  }

  checkInitialAuth(): Observable<boolean> {

    const token = this.tokenService.accessToken();
    const refreshToken = this.tokenService.refreshToken();

    if (!token || !refreshToken) {
      return of(false);
    }

    const decoded = this.tokenService.decodeToken();
    console.log(decoded);
    if (!decoded || !decoded['exp']) {
      return of(false);
    }

    const exp = (decoded['exp'] as number) * 1000;
    const now = Date.now();

    const isExpired = exp < now;

    if (!isExpired) {
      return of(true);
    }
    return this.refreshToken({ token, refreshToken }).pipe(
      tap(res => this.tokenService.setTokens(res)),
      map(() => true),
      catchError(() => of(false))
    )

  }

  register(payload: RegisterRequest): Observable<void> {
    return this.http.post<void>(`${this.base}/Register`, payload);
  }

  refreshToken(payload: RefreshTokenRequest): Observable<AuthResponse> {
    return this.http.post<AuthResponse>(`${this.base}/RefreshAuth`, payload).pipe(
      tap((response) => this.tokenService.setTokens(response))
    );
  }

  confirmEmail(payload: EmailConfirmRequest): Observable<void> {
    return this.http.post<void>(`${this.base}/ConfirmEmail`, payload);
  }

  resendConfirmationEmail(payload: ResendConfirmationRequest): Observable<void> {
    return this.http.post<void>(`${this.base}/ResendConfirmEmail`, payload);
  }

  forgotPassword(email: string): Observable<void> {
    const params = new HttpParams().set('Email', email);
    return this.http.post<void>(`${this.base}/ForgetUserPassword`, null, { params });
  }

  resetPassword(payload: ResetPasswordRequest): Observable<void> {
    return this.http.post<void>(`${this.base}/ResetPassword`, payload);
  }

  logout(): void {
    this.tokenService.clearTokens();
    this.router.navigate(['/auth/login']);
  }

  hasRole(role: string): boolean {
    return this.tokenService.hasRole(role);
  }
}
