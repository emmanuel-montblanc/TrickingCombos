import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { signal } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../Services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';

@Component({
  selector: 'app-login-page',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatIcon
  ],
  templateUrl: './login-page.component.html',
  styleUrl: './login-page.component.scss'
})
export class LoginPageComponent {
  form: FormGroup;
  returnUrl: string = '/';
  loginHasFailed = signal(false);
  hidePassword = signal(true);

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router
  ) { 
    this.form = this.fb.group({
      username: '',
      password: ''
    });
    this.returnUrl = this.route.snapshot.queryParamMap.get('returnUrl') || '/';

    if (this.authService.isLoggedIn()) 
      this.router.navigateByUrl(this.returnUrl);
  }

  hidePasswordClicked(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }

  hideLoginErrorClicked(event: MouseEvent) {
    this.loginHasFailed.set(false);
    event.stopPropagation();
  }

  login() {
    console.log(this.form.value);

    this.authService.login(this.form.value.username, this.form.value.password).subscribe({
      next: () => {
        this.router.navigateByUrl(this.returnUrl);
      },
      error: err => {
        console.error('Login failed', err);
        this.form.get('password')?.reset();
        this.loginHasFailed.set(true);
      }
    })
  }
}
