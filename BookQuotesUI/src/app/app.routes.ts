import { Routes } from '@angular/router';

export const routes: Routes = [
  {path: 'login', loadComponent: () => import('./login/login').then(m => m.Login), title: 'Login - Book Quotes' },
  {path: 'authors', loadComponent: () => import('./authors/authors').then(m => m.Authors), title: 'Authors - Book Quotes' },
  {path: 'selection', loadComponent: () => import('./selection/selection').then(m => m.Selection), title: 'Selection - Book Quotes' },
  {path: 'books', loadComponent: () => import('./books/books').then(m => m.Books), title: 'Books - Book Quotes' },
  {path: 'quotes', loadComponent: () => import('./quotes/quotes').then(m => m.Quotes), title: 'Quotes - Book Quotes' },
  { path: '', redirectTo: '/login', pathMatch: 'full', title: 'Login - Book Quotes' } // Redirect to login by default
];
