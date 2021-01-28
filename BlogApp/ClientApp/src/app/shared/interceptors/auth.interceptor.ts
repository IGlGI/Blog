import {Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {catchError} from 'rxjs/operators';
import {HttpErrorResponse, HttpEvent, HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {Observable, throwError} from 'rxjs';
import {AuthService} from '../../admin/shared/services/auth.service';

@Injectable()
export class AuthInterceptor implements HttpInterceptor{

  constructor(
    private authService: AuthService,
    private router: Router
  ) {
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    if (this.authService.isAuthenticated()){
      req = req.clone({
        headers: this.authService.getHeaders()
      });
    }
    return next.handle(req)
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
}
