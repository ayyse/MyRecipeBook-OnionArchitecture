import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { DynamicDialogConfig, DynamicDialogRef } from 'primeng/dynamicdialog';
import { CategoryDto, Client, CreateCategoryDto } from '../../../../api/api';
import { SelectModule } from 'primeng/select';
import { ButtonModule } from 'primeng/button';
import { CommonModule } from '@angular/common';
import { InputTextModule } from 'primeng/inputtext';
import { TextareaModule } from 'primeng/textarea';

@Component({
  selector: 'app-admin-category-form-component',
  standalone: true,
  imports: [CommonModule, SelectModule, ButtonModule, ReactiveFormsModule, InputTextModule, TextareaModule],
  templateUrl: './admin-category-form-component.html',
  styleUrl: './admin-category-form-component.css',
})
export class AdminCategoryFormComponent implements OnInit {
  form!: FormGroup;
  parentCategories: CategoryDto[] = [];
  isUpdate: boolean = false;

  constructor(
    private fb: FormBuilder,
    private ref: DynamicDialogRef,
    private client: Client,
    public config: DynamicDialogConfig, // Dışarıdan gelen veriyi tutar (parent componentten category bilgisini aldım)
  ) { }

  ngOnInit(): void {
    const editData = this.config.data;
    this.isUpdate = !!editData;

    this.form = this.fb.group({
      id: [editData?.id || null],
      name: [editData?.name || '', [Validators.required]],
      description: [editData?.description || ''],
      parentCategoryId: [editData?.parentCategoryId || null]
    });

    this.loadParentCategories();
  }

  loadParentCategories() {
    this.client.parentCategories().subscribe(res => this.parentCategories = res);
  }

  save() {
    if (this.form.invalid) return;

    const payload = this.form.value;

    if (this.isUpdate) {
      console.log("edit", payload)
      this.client.categoryPUT(payload.id, payload).subscribe({
        next: () => this.ref.close(true),
        error: (err) => console.error(err)
      });
    } else {
      console.log("create", payload)
      this.client.categoryPOST(payload).subscribe({
        next: () => this.ref.close(true),
        error: (err) => console.error(err)
      });
    }
  }

  close() {
    this.ref.close();
  }
}