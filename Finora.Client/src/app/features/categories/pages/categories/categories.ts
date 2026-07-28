import { Component, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CategoryCardComponent } from '../../components/category-card/category-card';
import { Category } from '../../../../core/models/category.model';
import { MatDialog } from '@angular/material/dialog';
import { AddCategoryDialogComponent } from '../../components/add-category-dialog/add-category-dialog';

@Component({
  selector: 'app-categories',
  imports: [MatIconModule, CategoryCardComponent, AddCategoryDialogComponent],
  templateUrl: './categories.html',
  styleUrl: './categories.scss',
})
export class CategoriesComponent {

  private readonly dialog = inject(MatDialog);

  openAddCategoryDialog(): void {

  this.dialog.open(AddCategoryDialogComponent, {

    width: '550px',

    maxWidth: '95vw',

    disableClose: true

  });

}

  categories: Category[] = [

{
id:1,
name:'Shopping',
type:'Expense',
icon:'shopping_cart'
},

{
id:2,
name:'Food',
type:'Expense',
icon:'restaurant'
},

{
id:3,
name:'Salary',
type:'Income',
icon:'payments'
},

{
id:4,
name:'Investment',
type:'Income',
icon:'trending_up'
}

];
}
