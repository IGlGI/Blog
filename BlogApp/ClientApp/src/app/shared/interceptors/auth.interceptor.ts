import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {catchError} from 'rxjs/operators';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpHeaders, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {AuthService} from '../../admin/shared/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{

  constructor(
    private authService: AuthService,
    private router: Router
  ) { }

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const headers = this.SetAuthHeader(request);
    request = request.clone({ headers });

    return next.handle(request)
      .pipe(
        catchError((error: HttpErrorResponse) => {
          console.warn('Interceptor error: ', error);
          if (error.status === 401) {
            this.authService.logout();
            this.router.navigate(['/admin', 'login'], {
              queryParams: {
                accessDenied: true
              }
            });
          }
          return throwError(error);
        })
      );
  }

  private SetAuthHeader(request: HttpRequest<any>): HttpHeaders {
    const token = this.authService.getAuthorizationHeaderValue();
    const headerSettings: {[name: string]: string | string[]; } = {};

    for (const key of request.headers.keys()) {
      headerSettings[key] = request.headers.getAll(key);
    }
    if (token) {
      headerSettings.Authorization = token;
    }

    headerSettings.accept = 'text/plain';
    return new HttpHeaders(headerSettings);
  }
}
