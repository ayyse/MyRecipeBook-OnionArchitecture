import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Client, RegisterDto } from '../../api/api';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-register-component',
  imports: [ReactiveFormsModule],
  templateUrl: './register-component.html',
  styleUrl: './register-component.css',
  standalone: true,
  providers: [Client],
})

export class RegisterComponent {
  registerForm = new FormGroup({
    email: new FormControl('', { nonNullable: true }),
    password: new FormControl('', { nonNullable: true }),
  });

  constructor(private client: Client, private router: Router, private authService: AuthService) {}


  submit() {
    if (this.registerForm.invalid) {
      console.log('Form invalid');
      return;
    }

    const registerDto: RegisterDto = {
      email: this.registerForm.value.email,
      password: this.registerForm.value.password,
    };

    console.log(registerDto)

    this.client.register(registerDto).subscribe({
      next: () => {
        console.log(registerDto)
        console.log('Kayıt başarılı!');
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.error('Kayıt hatası:', err);
      },
    });
  }

}
