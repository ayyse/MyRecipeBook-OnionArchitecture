import { Component } from '@angular/core';
import { CategoryComponent } from "../category-component/category-component";

@Component({
  selector: 'app-home-component',
  imports: [CategoryComponent],
  templateUrl: './home-component.html',
  styleUrl: './home-component.css',
})
export class HomeComponent {
  
}
