import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryDto, Client } from '../../../api/api';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-category-component',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './user-category-component.html',
  styleUrl: './user-category-component.css',
})
export class UserCategoryComponent implements OnInit {
  categories: CategoryDto[] = [];
  
  constructor(private client: Client, private cdr: ChangeDetectorRef) { }

  ngOnInit(): void {
    this.getParentCategories();
  }

  getParentCategories(): void {
    this.client.parentCategories().subscribe({
      next: (response: CategoryDto[]) => {
        this.categories = response;
        this.cdr.detectChanges();
        console.log(this.categories)
      },
      error: (err) => {
        console.error('Kategori çekilirken hata oluştu', err);
      }
    });
  }
}

