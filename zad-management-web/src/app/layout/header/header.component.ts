import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { StateService } from '../../core/services/state.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [CommonModule, RouterModule, FormsModule],
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent {
  state = inject(StateService);
  router = inject(Router);

  onCompanyChange(companyIdStr: string): void {
    const companyId = parseInt(companyIdStr, 10);
    if (!isNaN(companyId)) {
      this.state.selectCompany(companyId);
    }
  }

  onBranchChange(branchIdStr: string): void {
    const branchId = parseInt(branchIdStr, 10);
    if (!isNaN(branchId)) {
      this.state.selectBranch(branchId);
    }
  }

  goToHome(): void {
    this.router.navigate(['/home']);
  }
}
