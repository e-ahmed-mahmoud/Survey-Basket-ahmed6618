import { Injectable, signal, computed } from '@angular/core';
import { AuthResponse } from "../../shared/models/Auth/AuthResponse";

@Injectable({ providedIn: 'root' })

export class TokenService {
  private _token = signal<string | null>(
    localStorage.getItem('token')
  );
  private readonly _refreshToken = signal<string | null>(
    localStorage.getItem('refreshToken')
  );

  readonly accessToken = computed(() => this._token());
  readonly refreshToken = computed(() => this._refreshToken());
  readonly isLoggedIn = computed(() => !!this._token());

  setTokens(auth: AuthResponse): void {
    this._token.set(auth.token);
    this._refreshToken.set(auth.refreshToken);
    localStorage.setItem('token', auth.token);
    localStorage.setItem('refreshToken', auth.refreshToken);
  }

  updateAccessToken(accessToken: string): void {
    this._token.set(accessToken);
  }

  clearTokens(): void {
    this._token.set(null);
    this._refreshToken.set(null);
    localStorage.removeItem('token');
    localStorage.removeItem('refreshToken');
  }

  decodeToken(): Record<string, unknown> | null {
    const token = this._token();
    if (!token) return null;
    try {
      const payload = token.split('.')[1];
      return JSON.parse(atob(payload));
    } catch {
      return null;
    }
  }

  getUserRoles(): string[] {
    const decoded = this.decodeToken();
    if (!decoded) return [];
    const roles = decoded['roles'];
    if (Array.isArray(roles)) return roles;
    if (typeof roles === 'string') return [roles];
    return [];
  }

  hasRole(role: string): boolean {
    return this.getUserRoles().includes(role);
  }
}
