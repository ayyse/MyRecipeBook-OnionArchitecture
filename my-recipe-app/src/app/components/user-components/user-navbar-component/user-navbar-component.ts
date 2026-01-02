import { Component } from '@angular/core';
import { AuthService } from '../../../services/auth-service';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-user-navbar-component',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './user-navbar-component.html',
  styleUrl: './user-navbar-component.css',
})
export class UserNavbarComponent {
  constructor(
    private authService: AuthService,
  ) { }

  get email(): string | null {
    return this.authService.getUserEmail();
  }

  get isLoggedIn(): boolean {
    return this.authService.isLoggedIn();
  }

  logout() {
    this.authService.logout();
  }
}
