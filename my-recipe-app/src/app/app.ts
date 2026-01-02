import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserNavbarComponent } from './components/user-components/user-navbar-component/user-navbar-component';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, UserNavbarComponent],
  templateUrl: './app.html',
  styleUrl: './app.css',
  standalone: true
})
export class App {
  protected readonly title = signal('my-recipe-app');
}
