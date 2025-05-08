import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { tap } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private path = environment.apiUrl;
  private tokenKey = 'authToken';
  private username = 'username';

  constructor(private http: HttpClient) { }

  login(username: string, password: string): Observable<any> {
    return this.http.post<{ token: string, username: string }>(this.path + 'users/login', { username, password})
      .pipe(tap(response => {
         localStorage.setItem(this.tokenKey, response.token);
         localStorage.setItem(this.username, response.username);
        }));
  }

  register(email: string, username: string, password: string): Observable<any> {
    return this.http.post<{ token: string, username: string }>(this.path + 'users/register', { email, username, password})
      .pipe(tap(response => {
         localStorage.setItem(this.tokenKey, response.token);
         localStorage.setItem(this.username, response.username);
        }));
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
    localStorage.removeItem(this.username);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  getUsername(): string | null {
    return localStorage.getItem(this.username);
  }

  isLoggedIn(): boolean {
    return !!this.getToken();
  }
}
