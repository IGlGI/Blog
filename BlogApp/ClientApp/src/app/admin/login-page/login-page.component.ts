import { Component, OnInit } from '@angular/core';
import {AuthService} from '../shared/services/auth.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-login-page',
  template: ''
})
export class LoginPageComponent implements OnInit{
  constructor(
    public authService: AuthService,
    private router: Router,
  ) {
  }

  ngOnInit(): void {
    if (this.authService.isLoggedIn()) {
      this.router.navigate(['/admin', 'dashboard']);
    }
    else {
      this.authService.startAuthentication();
    }
  }
}
