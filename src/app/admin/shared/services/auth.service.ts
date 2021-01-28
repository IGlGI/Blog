import { Injectable } from '@angular/core';
import {HttpClient, HttpErrorResponse} from '@angular/common/http';
import {Observable, Subject, throwError} from 'rxjs';
import {catchError, tap} from 'rxjs/operators';

import {User} from '../../../shared/models/user.model';
import {environment} from '../../../../environments/environment';
import {FbAuthResponse} from '../../../shared/models/fb-auth-response.model';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  public error$: Subject<string> = new Subject<string>();

  get token(): string | null {
    const token = localStorage.getItem('fb-token');
    const date = localStorage.getItem('fb-token-exp');

    if (token && date) {
      const expireDate = new Date(date);
      if (new Date() > expireDate){
        this.logout();
        return null;
      }
      return token;
    }
    return null;
  }

  constructor(private http: HttpClient) { }

  login(user: User): Observable<any> {
    user.returnSecureToken = true;
    return this.http.post<FbAuthResponse>(
      `https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${environment.apiKey}`, user)
      .pipe(
        tap(this.setToken),
        catchError(this.handleError.bind(this))
      );
  }

  logout(): void {
    this.setToken(null);
  }

  isAuthenticated(): boolean {
    return !!this.token;
  }

  private handleError(error: HttpErrorResponse): any {
    const {message} = error.error.error;

    switch (message) {
      case 'INVALID_EMAIL':
        this.error$.next('Invalid email.');
        break;
      case 'INVALID_PASSWORD':
        this.error$.next('Invalid password.');
        break;
      case 'EMAIL_NOT_FOUND':
        this.error$.next('Email not found.');
        break;
    }

    return throwError(error);
  }

  private setToken(response: FbAuthResponse | null): void {
    if (response) {
      const expiresDate = new Date( new Date().getTime() + +response.expiresIn * 1000);
      localStorage.setItem('fb-token', response.idToken);
      localStorage.setItem('fb-token-exp', expiresDate.toString());
    }
    else {
      localStorage.clear();
    }
  }
}
