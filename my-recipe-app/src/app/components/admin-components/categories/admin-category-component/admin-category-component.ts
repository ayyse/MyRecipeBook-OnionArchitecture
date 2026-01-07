import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { Client } from '../../../../api/api';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-admin-category-component',
  imports: [CommonModule],
  templateUrl: './admin-category-component.html',
  styleUrl: './admin-category-component.css',
})
export class AdminCategoryComponent implements OnInit {
  categories: any[] = [];
    expandedIds = new Set<number>();


  constructor(private client: Client, private cdr: ChangeDetectorRef) { }

  ngOnInit() {
    this.getCategories();
  }

  getCategories() {
    this.client.parentCategories().subscribe(res => {
      this.categories = res;
      this.cdr.detectChanges();
      console.log(this.categories)
    });
  }

  delete(id: string) {
    if (!confirm('Bu kategoriyi silmek istiyor musun?')) return;

    this.client.categoryDELETE(id).subscribe(() => {
      this.categories = this.categories.filter(x => x.id !== id);
    });
  }

  toggle(id: number) {
    this.expandedIds.has(id)
      ? this.expandedIds.delete(id)
      : this.expandedIds.add(id);
  }

  isExpanded(id: number): boolean {
    return this.expandedIds.has(id);
  }
}
