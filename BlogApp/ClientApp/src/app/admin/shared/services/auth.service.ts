import {Injectable, OnDestroy, OnInit} from '@angular/core';
import {OidcClientNotification, OidcSecurityService, PublicConfiguration} from 'angular-auth-oidc-client';
import {Observable, Subscription} from 'rxjs';
import { HttpHeaders } from '@angular/common/http';
import {environment} from '../../../../environments/environment';
import {CookieService} from 'ngx-cookie-service';
import {ActivatedRoute, Router} from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  configuration: PublicConfiguration;

  constructor(
    public oidcSecurityService: OidcSecurityService,
    private cookieService: CookieService,
    private router: Router
  ) {
    this.configuration = this.oidcSecurityService.configuration;
    this.isAuthenticated();
  }

  login(): void {
    this.oidcSecurityService.authorize();
  }

  logout(): void {
    this.oidcSecurityService.logoff();
    this.router.navigate(['/']);
    console.log('Logged off ', this.oidcSecurityService.getState());
  }

  isAuthenticated(): boolean {
    const authSession = this.cookieService.get('idsrv.session');
    if (this.oidcSecurityService.getState() && authSession) {
      return true;
    }
    return false;
  }

  public getHeaders(): HttpHeaders {
    let headers = new HttpHeaders();
    headers = headers.set('Content-Type', 'application/json');
    return this.appendAuthHeader(headers);
  }

  public getToken(): string {
    const token = this.oidcSecurityService.getToken();
    console.log('TOKEN: ', token);
    return token;
  }

  private appendAuthHeader(headers: HttpHeaders): HttpHeaders {
    const token = this.oidcSecurityService.getToken();

    if (token === '') { return headers; }

    const tokenValue = 'Bearer ' + token;
    return headers.set('Authorization', tokenValue);
  }

}
