import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';

interface AppItem {
  id: string;
  title: string;
  route?: string;
  iconBg: string;
  iconSvg: string;
  badge?: string;
}

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  apps: AppItem[] = [
    {
      id: 'settings',
      title: 'Settings',
      route: '/settings/companies',
      iconBg: '#ffffff',
      iconSvg: 'settings'
    },
    {
      id: 'discuss',
      title: 'Discuss',
      iconBg: '#ffffff',
      iconSvg: 'discuss'
    },
    {
      id: 'accounting',
      title: 'Accounting',
      iconBg: '#ffffff',
      iconSvg: 'accounting'
    },
    {
      id: 'inventory',
      title: 'Inventory',
      iconBg: '#ffffff',
      iconSvg: 'inventory'
    },
    {
      id: 'purchase',
      title: 'Purchase',
      iconBg: '#ffffff',
      iconSvg: 'purchase'
    },
    {
      id: 'sales',
      title: 'Sales',
      iconBg: '#ffffff',
      iconSvg: 'sales'
    },
    {
      id: 'vehicle-rental',
      title: 'Vehicle Rental',
      route: '/rentals/contracts',
      iconBg: '#ffffff',
      iconSvg: 'rental'
    },
    {
      id: 'employees',
      title: 'Employees',
      iconBg: '#ffffff',
      iconSvg: 'employees'
    }
  ];

  constructor(private router: Router) {}

  onAppClick(app: AppItem): void {
    if (app.route) {
      this.router.navigate([app.route]);
    }
  }
}

