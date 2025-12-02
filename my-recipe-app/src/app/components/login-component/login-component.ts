import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { Client, LoginDto } from '../../api/api';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-login-component',
  imports: [ReactiveFormsModule],
  templateUrl: './login-component.html',
  styleUrl: './login-component.css',
  standalone: true
})
export class LoginComponent {
  loginForm = new FormGroup({
    email: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  constructor(private router: Router, private client: Client, private authService: AuthService) {}

  login() {
    if (this.loginForm.invalid) {
          console.log('Form invalid');
          return;
        }
    
        const loginDto: LoginDto = {
          email: this.loginForm.value.email,
          password: this.loginForm.value.password,
        };
        
        this.client.login(loginDto).subscribe({
          next: () => {
            console.log('Kayıt başarılı!');
            this.router.navigate(['/home']);
            this.authService.setUser({ email: loginDto.email });
          },
          error: (err) => {
            console.error('Kayıt hatası:', err);
          },
        });
  }
}
