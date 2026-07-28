import { Component, input } from '@angular/core';
import { MatIcon, MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-recent-transactions',
  standalone: true,
  imports: [MatIconModule],
  templateUrl: './recent-transactions.html',
  styleUrl: './recent-transactions.scss',
})
export class RecentTransactionsComponent {
  readonly transactions = input.required<any[]>();
}
