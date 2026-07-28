import { CommonModule } from '@angular/common';
import { Component, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { LoginRequest } from '../../models/login-request.model';
import { AuthService } from '../../services/auth.service';
import { StorageKey } from '../../../../core/constants/storage-keys';
import { StorageService } from '../../../../core/services/storage.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,

    MatCardModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule
  ],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {

  private readonly fb = inject(FormBuilder);
  private readonly authService = inject(AuthService);
  private readonly storageService = inject(StorageService)
  // private readonly router = inject(Router);

  readonly hidePassword = signal(true);

  readonly isLoading = signal(true);

  get email()
  {
      return this.loginForm.controls.email;
  }

  get password()
  {
      return this.loginForm.controls.password;
  }

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(8)]],
    rememberMe: [false]
  });

  togglePassword(): void {
    this.hidePassword.update(value => !value);
  }

  onSubmit(): void
  {
    if(this.loginForm.invalid)
    {
      this.loginForm.markAllAsTouched();
      return;
    }

    this.isLoading.set(true);

    const request: LoginRequest = {
      email: this.loginForm.value.email!,
      password: this.loginForm.value.password!
    };

    this.authService.login(request).subscribe({
      next: (response) => {
        
        this.storageService.saveAccessToken(response.accessToken);
        this.storageService.saveRefreshToken(response.refreshToken);
        
        console.log(response);
        
        this.isLoading.set(false);
        // this.router.navigate(['/dashboard']);
        
      },

      error: (error) => {
        console.error('login failed');
        
        console.error(error);
        this.isLoading.set(false);
        
      },

      complete: () => {
        this.isLoading.set(false);
      }
    });
  }
}