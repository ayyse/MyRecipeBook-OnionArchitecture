import { Component, OnInit } from '@angular/core';
import { Client } from '../../../../api/api';
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { TreeTableModule } from 'primeng/treetable';
import { TreeNode } from 'primeng/api';

@Component({
  selector: 'app-admin-category-component',
  imports: [CommonModule, ButtonModule, TreeTableModule],
  templateUrl: './admin-category-component.html',
  styleUrl: './admin-category-component.css',
})
export class AdminCategoryComponent implements OnInit {
  categories: TreeNode[] | null = null;

  constructor(private client: Client) { }

  ngOnInit() {
    this.getParentCategories();
  }

  getParentCategories() {
    this.client.parentCategories().subscribe(res => {
      this.categories = [...this.mapToTreeNode(res)];
      console.log(this.categories)
    });
  }

  mapToTreeNode(categories: any[]): TreeNode[] {
    return categories.map(c => ({
      data: {
        id: c.id,
        name: c.name,
        description: c.description
      },
      children: c.subCategories?.length
        ? this.mapToTreeNode(c.subCategories)
        : []
    }));
  }

  // delete(id: string) {
  //   if (!confirm('Bu kategoriyi silmek istiyor musun?')) return;

  //   this.client.categoryDELETE(id).subscribe(() => {
  //     this.categories = this.categories.filter(x => x.id !== id);
  //   });
  // }
}
