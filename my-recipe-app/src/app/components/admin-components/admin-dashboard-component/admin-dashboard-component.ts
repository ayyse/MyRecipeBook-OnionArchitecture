import { Component } from '@angular/core';

@Component({
  selector: 'app-admin-dashboard-component',
  imports: [],
  templateUrl: './admin-dashboard-component.html',
  styleUrl: './admin-dashboard-component.css',
})
export class AdminDashboardComponent {
  totalRecipes = 128;
  totalCategories = 12;
  totalUsers = 45;
  averageRating = 4.6;


  latestRecipes = [
  { name: 'Karnıyarık', categoryName: 'Ana Yemek', creationTime: new Date() },
  { name: 'Mercimek Çorbası', categoryName: 'Çorba', creationTime: new Date() }
  ];
}
