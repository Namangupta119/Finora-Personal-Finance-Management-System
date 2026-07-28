import { Component, inject } from '@angular/core';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatRadioModule } from '@angular/material/radio';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { CategoryServices } from '../../../../core/services/category.services';

@Component({
  selector: 'app-add-category-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatRadioModule,
    MatSelectModule,
    MatButtonModule,
    ReactiveFormsModule,
  ],
  templateUrl: './add-category-dialog.html',
  styleUrl: './add-category-dialog.scss'
})
export class AddCategoryDialogComponent {
  private readonly fb = inject(FormBuilder).nonNullable;;
  private readonly categoryServices = inject(CategoryServices);
  private readonly dialogRef = inject(MatDialogRef<AddCategoryDialogComponent>);

  readonly categoryForm = this.fb.group({
    name: ['', Validators.required],
    description: [''],
    iconKey: ['restaurant', Validators.required],
    colorKey: ['red', Validators.required]
  });

  saveCategory(): void {

     if (this.categoryForm.invalid) {
    return;
  }

  const request = this.categoryForm.getRawValue();

  this.categoryServices.createCategory(request).subscribe({

    next: (id) => {

      console.log('Category Created');

      console.log(id);
      this.dialogRef.close(true);

    },

    error: (error) => {

      console.error(error);

    }
  });

}
}