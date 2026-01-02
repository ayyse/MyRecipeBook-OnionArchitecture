import { Component } from '@angular/core';
import { AdminSidebarComponent } from '../admin-sidebar-component/admin-sidebar-component';

@Component({
  selector: 'app-admin-home-component',
  imports: [AdminSidebarComponent],
  templateUrl: './admin-home-component.html',
  styleUrl: './admin-home-component.css',
})
export class AdminHomeComponent {
  totalRecipes = 128;
  totalCategories = 12;
  totalUsers = 45;
  averageRating = 4.6;


  latestRecipes = [
  { name: 'Karnıyarık', categoryName: 'Ana Yemek', creationTime: new Date() },
  { name: 'Mercimek Çorbası', categoryName: 'Çorba', creationTime: new Date() }
  ];
}
