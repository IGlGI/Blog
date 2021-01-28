import { Component, OnInit } from '@angular/core';
import {AuthService} from '../shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login-page',
  template: ''
})
export class LoginPageComponent {
  constructor(
    public authService: AuthService,
    private router: Router,
  ) {
    if (this.authService.isAuthenticated()) {
      this.router.navigate(['/admin', 'dashboard']);
    }
    else {
      this.authService.login();
    }
  }
}
