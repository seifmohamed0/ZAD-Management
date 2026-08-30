import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: '',
    redirectTo: 'home',
    pathMatch: 'full'
  },
  {
    path: 'home',
    loadComponent: () => import('./features/home/home.component').then(m => m.HomeComponent)
  },
  {
    path: 'settings/companies',
    loadComponent: () => import('./features/settings/companies/companies-list.component').then(m => m.CompaniesListComponent)
  },
  {
    path: 'settings/branches',
    loadComponent: () => import('./features/settings/branches/branches-list.component').then(m => m.BranchesListComponent)
  },
  {
    path: 'rentals/contracts',
    loadComponent: () => import('./features/rentals/contracts/contracts-list.component').then(m => m.ContractsListComponent)
  },
  {
    path: 'rentals/contracts/add',
    loadComponent: () => import('./features/rentals/contracts/add-contract.component').then(m => m.AddContractComponent)
  },
  {
    path: '**',
    redirectTo: 'home'
  }
];
