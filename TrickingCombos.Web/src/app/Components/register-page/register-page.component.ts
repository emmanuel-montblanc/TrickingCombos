import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { signal } from '@angular/core';
import { FormBuilder, FormGroup, AbstractControl, ReactiveFormsModule, Validators, ValidationErrors, FormControl } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { AuthService } from '../../Services/auth.service';
import { ActivatedRoute, Router } from '@angular/router';
import {MatCardModule} from '@angular/material/card';
import { MatIcon } from '@angular/material/icon';
import { Observable, of } from 'rxjs';
import { debounceTime, switchMap, map, catchError } from 'rxjs/operators';
import { SnackbarService } from '../../Services/snackbar.service';

@Component({
  selector: 'app-register-page',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatIcon
  ],
  templateUrl: './register-page.component.html',
  styleUrl: './register-page.component.scss'
})
export class RegisterPageComponent {
  form: FormGroup;
  hidePassword = signal(true);
  hideConfirmPassword = signal(true);

  constructor(
    private authService: AuthService,
    private fb: FormBuilder,
    private route: ActivatedRoute,
    private router: Router,
    private snackbar: SnackbarService
  ) { 
    this.form = this.fb.group({
      email: ['', [Validators.required, Validators.email], [this.validateEmail.bind(this)]],
      username: ['', [Validators.required], [this.validateUserName.bind(this)]],
      password:['', [Validators.required, this.validatePasswordStrength]],
      confirmPassword: ['', [Validators.required, this.validateSamePassword]]
    });

    // Pour checker si les mdps sont les même lors d'une modification du mdp original
    this.form.get('password')?.valueChanges.subscribe(() => {
      this.form.get('confirmPassword')?.updateValueAndValidity();
    });
    
  }

  validateEmail(control: AbstractControl): Observable<ValidationErrors | null> {
    if (!control.value) {
      return of(null);
    }
  
    return of(control.value).pipe(
      debounceTime(500),
      switchMap(email => {
        return this.authService.checkEmail(email).pipe(
          map(res => res.available ? null : { emailTaken: true }),
          catchError(() => of(null))
        )
      })
    );
  }

  validateUserName(control: AbstractControl): Observable<ValidationErrors | null> {
    if (!control.value) {
      return of(null);
    }
  
    return of(control.value).pipe(
      debounceTime(500),
      switchMap(username => {
        return this.authService.checkUsername(username).pipe(
          map(res => res.available ? null : { usernameTaken: true }),
          catchError(() => of(null))
        )
      })
    );
  }

  validatePasswordStrength(control: AbstractControl): ValidationErrors | null {
    const value = control.value || '';

    const hasUpperCase = /[A-Z]/.test(value);
    const hasLowerCase = /[a-z]/.test(value);
    const hasNumeric = /[0-9]/.test(value);
    const hasSpecialChar = /[!@#$%^&*(),.?":{}|<>]/.test(value);
    const hasMinLength = value.length >= 8;

    const passwordValid = hasUpperCase && hasLowerCase && hasNumeric && hasSpecialChar && hasMinLength;

    return passwordValid ? null : {
      passwordStrength: {
        hasUpperCase,
        hasLowerCase,
        hasNumeric,
        hasSpecialChar,
        hasMinLength
      }
    };
  };

  getPasswordError() {
    const passwordStrength = this.form.get('password')?.errors?.['passwordStrength'];
    let errorMsg = 'Le mot de passe doit contenir au moins ';
    if (!passwordStrength.hasUpperCase) {
      return errorMsg + 'une majuscule';
    } else if (!passwordStrength.hasLowerCase) {
      return errorMsg + 'une minuscule';
    } else if (!passwordStrength.hasNumeric) {
      return errorMsg + 'un chiffre';
    } else if (!passwordStrength.hasSpecialChar) {
      return errorMsg + 'un caractère spécial';
    } else if (!passwordStrength.hasMinLength) {
      return errorMsg + ' 8 caractères';
    }
    return '';
  }

  validateSamePassword(control: AbstractControl): ValidationErrors | null {
    const password = control.parent?.get('password');
    const confirmPassword = control.parent?.get('confirmPassword');
    return password?.value == confirmPassword?.value ? null : { 'notSamePassword': true };
  }

  hidePasswordClicked(event: MouseEvent) {
    this.hidePassword.set(!this.hidePassword());
    event.stopPropagation();
  }
  
  hideConfirmPasswordClicked(event: MouseEvent) {
    this.hideConfirmPassword.set(!this.hideConfirmPassword());
    event.stopPropagation();
  }

  register() {
    console.log(this.form.value);

    this.authService.register(
      this.form.value.email,
      this.form.value.username,
      this.form.value.password
    ).subscribe({
      next: () => {
        this.router.navigateByUrl('/');
        this.snackbar.showSuccess('Votre compte à bien été créer !');
      },
      error: err => {
        console.error('register failed', err);
        this.snackbar.showError(`Une erreur est survenue lors de la création du compte`);
      }
    });
  }
}