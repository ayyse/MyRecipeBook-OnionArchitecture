import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserNavbarComponent } from './components/user-components/user-navbar-component/user-navbar-component';
import { AdminNavbarComponent } from './components/admin-components/admin-navbar-component/admin-navbar-component';
import { AuthService } from './services/auth-service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, AdminNavbarComponent, UserNavbarComponent, CommonModule],
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: true
})
export class App {
  protected readonly title = signal('my-recipe-app');

  constructor(
    private authService: AuthService,
  ) { }

  isAdmin(): boolean {
    const role = this.authService.getUserRole();
    console.log("ksdjf", role)
    return role === 'Admin';
  }
}
