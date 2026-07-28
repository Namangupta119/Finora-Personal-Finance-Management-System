import { Component } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { RouterLink, RouterLinkActive } from '@angular/router';



interface MenuItem {
  label: string;
  icon: string;
  route: string;
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    MatIconModule
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.scss',
})
export class SidebarComponent {
  
  menuItems: MenuItem[] = [
  {
    label: 'Dashboard',
    icon: 'dashboard',
    route: '/dashboard'
  },
  {
    label: 'Categories',
    icon: 'category',
    route: '/categories'
  },
  {
    label: 'Income',
    icon: 'payments',
    route: '/income'
  },
  {
    label: 'Expenses',
    icon: 'receipt_long',
    route: '/expenses'
  },
  {
    label: 'Budgets',
    icon: 'account_balance_wallet',
    route: '/budgets'
  },
  {
    label: 'Goals',
    icon: 'flag',
    route: '/goals'
  },
  {
    label: 'Investments',
    icon: 'trending_up',
    route: '/investments'
  }
];
}
