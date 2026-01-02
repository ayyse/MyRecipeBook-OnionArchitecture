import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Client, LoginDto } from '../../api/api';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login-component',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
})
export class LoginComponent {

  loginForm = new FormGroup({
    email: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  constructor(
    private router: Router,
    private client: Client,
    private authService: AuthService
  ) { }

  login() {
    localStorage.removeItem('token');
    
    if (this.loginForm.invalid) {
      return;
    }

    const loginDto: LoginDto = {
      email: this.loginForm.controls.email.value,
      password: this.loginForm.controls.password.value,
    };

    this.client.login(loginDto).subscribe({
      next: (response) => {

        console.log("responseee", response)

        if (!response.token) {
          console.error('Token response içinde yok');
          return;
        }

        this.authService.saveToken(response.token);

        if (this.authService.isAdmin()) {
          this.router.navigate(['/admin']);
        } else {
          this.router.navigate(['/']);
        }
      },
      error: (err) => {
        console.error('Login hatası:', err);
      }
    });
  }
}
