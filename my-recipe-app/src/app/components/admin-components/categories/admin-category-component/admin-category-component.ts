import { ChangeDetectorRef, Component, OnInit } from '@angular/core';
import { CategoryDto, Client } from '../../../../api/api'; // API yolunu kendine göre ayarla
import { CommonModule } from '@angular/common';
import { ButtonModule } from 'primeng/button';
import { TreeTableModule } from 'primeng/treetable';
import { ConfirmationService, MessageService, TreeNode } from 'primeng/api';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { AdminCategoryFormComponent } from '../admin-category-form-component/admin-category-form-component';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { ToastModule } from 'primeng/toast';

@Component({
  selector: 'app-admin-category-component',
  standalone: true,
  imports: [CommonModule, ButtonModule, TreeTableModule, ConfirmDialogModule, ToastModule], // DialogModule'ü kaldırdık, gerek yok.
  providers: [DialogService, ConfirmationService, MessageService], // Standalone component olduğu için servisi buraya eklemelisin.
  templateUrl: './admin-category-component.html',
  styleUrl: './admin-category-component.css',
})
export class AdminCategoryComponent implements OnInit {
  categories: TreeNode[] = [];

  constructor(
    private client: Client,
    private cdr: ChangeDetectorRef,
    private dialogService: DialogService,
    private confirmationService: ConfirmationService,
    private messageService: MessageService
  ) { }

  ngOnInit() {
    this.getParentCategories();
  }

  getParentCategories() {
    this.client.parentCategories().subscribe(res => {
      this.categories = [...this.mapToTreeNode(res)];
      this.cdr.detectChanges();
    });
  }

  mapToTreeNode(categories: any[]): TreeNode[] {
    return categories.map(c => ({
      data: {
        id: c.id,
        name: c.name,
        description: c.description,
        subCategories: c.subCategories
      },
      children: c.subCategories?.length
        ? this.mapToTreeNode(c.subCategories)
        : []
    }));
  }

  openCreateModal() {
    const ref = this.dialogService.open(AdminCategoryFormComponent, {
      header: 'Yeni Kategori Oluştur',
      width: '500px'
    });

    ref?.onClose.subscribe((result: any) => {
      // Eğer ref.close(true) gelmişse kayıt başarılıdır
      if (result) {
        this.getParentCategories();
      }
    });
  }

  openEditModal(category: CategoryDto) {
    const ref = this.dialogService.open(AdminCategoryFormComponent, {
      header: 'Kategoriyi Düzenle',
      width: '500px',
      data: category
    });

    ref?.onClose.subscribe((result: any) => {
      if (result) {
        this.getParentCategories();
      }
    });
  }

  delete(category: any) {
    // TreeTable'da çocuk kontrolünü node.children üzerinden yapmak en doğrusudur
    const hasChildren = category.subCategories && category.subCategories.length > 0;

    const message = hasChildren
      ? `"${category.name}" kategorisinin alt kategorileri bulunmaktadır. Silmeniz durumunda tüm alt kategoriler de silinecektir. Devam etmek istiyor musunuz?`
      : `"${category.name}" kategorisini silmek istediğinize emin misiniz?`;

    this.confirmationService.confirm({
      header: 'Silme Onayı',
      message: message,
      icon: 'pi pi-exclamation-triangle',
      acceptLabel: 'Evet, Sil',
      rejectLabel: 'Vazgeç',
      acceptButtonStyleClass: 'p-button-danger',
      accept: () => {
        this.executeDelete(category.id);
      }
    });
  }

  private executeDelete(id: string) {
    this.client.categoryDELETE(id).subscribe({
      next: () => {
        this.getParentCategories();
        this.messageService.add({ severity: 'success', summary: 'Başarılı', detail: 'Kategori silindi.' });
      },
      error: (err) => {
        this.messageService.add({ severity: 'error', summary: 'Hata', detail: 'Silme işlemi başarısız.' });
        console.error(err);
      }
    });
  }

}