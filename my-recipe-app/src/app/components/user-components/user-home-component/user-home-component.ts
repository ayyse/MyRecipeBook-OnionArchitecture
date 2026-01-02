import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Client, RecipeDto } from '../../../api/api';
import { UserCategoryComponent } from '../user-category-component/user-category-component';

@Component({
  selector: 'app-user-home-component',
  standalone: true,
  imports: [UserCategoryComponent],
  templateUrl: './user-home-component.html',
  styleUrl: './user-home-component.css',
})
export class UserHomeComponent implements OnInit {
  popularRecipes: RecipeDto[] = [];
  
    constructor(private client: Client, private cdr: ChangeDetectorRef) {}
  
    ngOnInit(): void {
      this.getPopularRecipes();
    }
    
    getPopularRecipes(): void {
      this.client.recipeAll().subscribe({
        next: (response: RecipeDto[]) => {
          this.popularRecipes = response;
          this.cdr.detectChanges();
  
          console.log('Recipes:', this.popularRecipes);
        },
        error: (err) => {
          console.error('Kategori çekilirken hata oluştu', err);
        }
      });
    }
}
