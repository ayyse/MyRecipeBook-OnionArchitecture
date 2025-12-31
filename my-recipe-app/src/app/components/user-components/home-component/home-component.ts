import { ChangeDetectorRef, Component } from '@angular/core';
import { CategoryComponent } from "../category-component/category-component";
import { Client, RecipeDto } from '../../../api/api';

@Component({
  selector: 'app-home-component',
  imports: [CategoryComponent],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {
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
