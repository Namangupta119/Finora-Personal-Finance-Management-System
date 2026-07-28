import { Component, input } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-category-card',
  standalone: true,
  imports: [MatIconModule],
  templateUrl: './category-card.html',
  styleUrl: './category-card.scss',
})
export class CategoryCardComponent {
  readonly name = input.required<string>();
  readonly type = input.required<'Income' | 'Expense'>();
  readonly icon = input.required<string>();
}
