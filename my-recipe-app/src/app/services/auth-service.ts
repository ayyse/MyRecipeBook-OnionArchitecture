import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

export interface UserInfo {
  email?: string;
}

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private readonly TOKEN_KEY = 'token';

  constructor(private router: Router) {}

  saveToken(token: string) {
    localStorage.setItem(this.TOKEN_KEY, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.TOKEN_KEY);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }

  private getPayload(): any | null {
    const token = this.getToken();
    if (!token) return null;
    return JSON.parse(atob(token.split('.')[1]));
  }

  getUserEmail(): string | null {
    return this.getPayload()?.email ??
      this.getPayload()?.[
        'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
      ] ?? null;
  }

  getUserRole(): string | null {
    return this.getPayload()?.[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ] ?? null;
  }

  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  logout() {
    localStorage.removeItem(this.TOKEN_KEY);
    this.router.navigate(['/login']);
  }
}