import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { tap } from 'rxjs/operators';
import { jwtDecode } from 'jwt-decode';

export interface JwtPayload {
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name": string;
  "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"?: string;
  "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"?: string;
  exp: number;
  iss: string;
  aud: string;
}


@Injectable({
  providedIn: 'root'
})

export class AuthService {
  private path = environment.apiUrl;
  private tokenKey = 'authToken';

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post<{ token: string, username: string }>(this.path + 'users/login', { username, password})
      .pipe(tap(response => { localStorage.setItem(this.tokenKey, response.token); }));
  }

  register(email: string, username: string, password: string): Observable<any> {
    return this.http.post<{ token: string }>(this.path + 'users/register', { email, username, password})
      .pipe(tap(response => { localStorage.setItem(this.tokenKey, response.token); }));
  }

  checkEmail(email: string): Observable<{ available: boolean }> {
    const encodedEmail = encodeURIComponent(email);
    return this.http.get<{ available: boolean }>(`${this.path}users/check-email?email=${encodedEmail}`);
  }

  checkUsername(username: string): Observable<{ available: boolean }> {
    const encodedUsername = encodeURIComponent(username);
    return this.http.get<{ available: boolean }>(`${this.path}users/check-username?username=${encodedUsername}`);
  }

  logout() {
    localStorage.removeItem(this.tokenKey);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getDecodedToken(): JwtPayload | null {
    const token = localStorage.getItem(this.tokenKey);
    if (!token) return null;
  
    try {
      return jwtDecode<JwtPayload>(token);
    } catch {
      return null;
    }
  }
  
  getUsername(): string | null {
    return this.getDecodedToken()?.["http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name"] ?? null;
  }

  getUserRole(): string | null {
    return this.getDecodedToken()?.["http://schemas.microsoft.com/ws/2008/06/identity/claims/role"] ?? null;
  }
  
  isAdmin(): boolean {
    return this.getUserRole() === 'Admin';
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
