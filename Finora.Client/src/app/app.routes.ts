import { Routes } from '@angular/router';
import { authGuard } from './features/auth/guards/auth.guard';

export const routes: Routes = [

  // Public Routes
  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/pages/login/login')
        .then(c => c.LoginComponent)
  },

  // Protected Routes
  {
    path: '',
    canActivate: [authGuard],
    loadComponent: () =>
      import('./layouts/main-layout/main-layout')
        .then(c => c.MainLayoutComponent),

    children: [

      {
        path: '',
        redirectTo: 'dashboard',
        pathMatch: 'full'
      },

      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/pages/dashboard/dashboard')
            .then(c => c.DashboardComponent)
      },

      {
        path: 'categories',
        loadComponent: () =>
          import('./features/categories/pages/categories/categories')
            .then(c => c.CategoriesComponent)
      }

    ]
  },

  // Wildcard Route
  {
    path: '**',
    redirectTo: 'login'
  }

];