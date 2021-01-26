import { Injectable } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {User} from '../../../shared/models/user.model';
import {Observable} from 'rxjs';
import {environment} from '../../../../environments/environment';
import {tap} from 'rxjs/operators';
import {FbAuthResponse} from '../../../shared/models/fb-auth-response.model';

@Injectable()
export class AuthService {

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
    return this.http.post<FbAuthResponse>(`https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key=${environment.apiKey}`, user)
      .pipe(
        tap(this.setToken)
      );
  }

  logout(): void {
    this.setToken(null);
  }

  isAuthenticated(): boolean {
    return !!this.token;
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
