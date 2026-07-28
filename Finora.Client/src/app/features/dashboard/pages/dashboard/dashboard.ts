import { Component } from '@angular/core';
import { SummaryCardComponent } from '../../components/summary-card/summary-card';
import { RecentTransactionsComponent } from '../../components/recent-transactions/recent-transactions';

@Component({
  selector: 'app-dashboard',
  imports: [SummaryCardComponent, RecentTransactionsComponent],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class DashboardComponent {
  userName = 'Naman';
  summaryCards = [
  {
    title: 'Total Balance',
    amount: '$0.00',
    icon: 'account_balance_wallet',
    color: 'bg-blue-100',
    iconColor: 'text-blue-600'
  },
  {
    title: 'Income',
    amount: '$0.00',
    icon: 'payments',
    color: 'bg-green-100',
    iconColor: 'text-green-600'
  },
  {
    title: 'Expenses',
    amount: '$0.00',
    icon: 'receipt_long',
    color: 'bg-red-100',
    iconColor: 'text-red-600'
  },
  {
    title: 'Savings',
    amount: '$0.00',
    icon: 'savings',
    color: 'bg-yellow-100',
    iconColor: 'text-yellow-600'
  }
];

  transactions = [

{
id:1,
title:'Salary',
category:'Income',
amount:2500,
date:'Today',
type:'income'
},

{
id:2,
title:'Amazon',
category:'Shopping',
amount:120,
date:'Yesterday',
type:'expense'
},

{
id:3,
title:'Netflix',
category:'Entertainment',
amount:15,
date:'2 days ago',
type:'expense'
}

];
}
