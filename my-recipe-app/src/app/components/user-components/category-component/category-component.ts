import { Component, OnInit } from '@angular/core';
import { CategoryDto, Client } from '../../../api/api';

@Component({
  selector: 'app-category-component',
  standalone: true,
  imports: [],
  templateUrl: './category-component.html',
  styleUrl: './category-component.css',
})
export class CategoryComponent implements OnInit {
  categories: CategoryDto[] = [];

  constructor(private client: Client) {}

  ngOnInit(): void {
    this.getParentCategories();
  }
  
  getParentCategories(): void {
    this.client.categoryAll().subscribe({
      next: (response: CategoryDto[]) => {
        // parentCategoryId null olanları al
        this.categories = response.filter(
          c => c.parentCategoryId === null
        );

        console.log('Parent categories:', this.categories);
      },
      error: (err) => {
        console.error('Kategori çekilirken hata oluştu', err);
      }
    });
  }

}
