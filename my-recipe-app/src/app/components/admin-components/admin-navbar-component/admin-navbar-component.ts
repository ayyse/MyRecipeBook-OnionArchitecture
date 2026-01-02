import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth-service';

@Component({
  selector: 'app-admin-navbar-component',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './admin-navbar-component.html',
  styleUrl: './admin-navbar-component.css',
})
export class AdminNavbarComponent {
  constructor(
    private authService: AuthService,
  ) { }

  get email(): string | null {
    return this.authService.getUserEmail();
  }

  logout() {
    this.authService.logout();
  }
}
