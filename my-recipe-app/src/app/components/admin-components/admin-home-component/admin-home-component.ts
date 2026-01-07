import { Component } from '@angular/core';
import { AdminSidebarComponent } from '../admin-sidebar-component/admin-sidebar-component';
import { RouterModule, RouterOutlet } from "@angular/router";

@Component({
  selector: 'app-admin-home-component',
  imports: [AdminSidebarComponent, RouterModule, RouterOutlet],
  templateUrl: './admin-home-component.html',
  styleUrl: './admin-home-component.css',
})
export class AdminHomeComponent {
  
}
