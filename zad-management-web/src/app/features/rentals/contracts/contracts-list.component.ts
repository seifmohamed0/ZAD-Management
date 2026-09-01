import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { ContractService } from '../../../core/services/contract.service';
import { StateService } from '../../../core/services/state.service';
import { RentalContractListDto, ContractStatus, ContractType } from '../../../core/models/contract.model';

@Component({
  selector: 'app-contracts-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './contracts-list.component.html',
  styleUrls: ['./contracts-list.component.scss']
})
export class ContractsListComponent implements OnInit {
  private contractService = inject(ContractService);
  public state = inject(StateService);
  private router = inject(Router);

  contracts = signal<RentalContractListDto[]>([]);
  selectedBranchFilter = signal<number | null>(null);
  searchTerm = signal<string>('');
  isLoading = signal<boolean>(false);

  ContractStatus = ContractStatus;
  ContractType = ContractType;

  ngOnInit(): void {
    this.loadContracts();
  }

  loadContracts(): void {
    this.isLoading.set(true);
    const branchId = this.selectedBranchFilter() || undefined;
    this.contractService.getAll(branchId).subscribe({
      next: (data) => {
        this.contracts.set(data);
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  get filteredContracts(): RentalContractListDto[] {
    const term = this.searchTerm().toLowerCase().trim();
    if (!term) return this.contracts();
    return this.contracts().filter(c =>
      c.contractNumber?.toLowerCase().includes(term) ||
      c.tenantName?.toLowerCase().includes(term) ||
      c.vehiclePlateNo?.toLowerCase().includes(term) ||
      c.tenantMobile?.includes(term) ||
      c.referenceNo?.toLowerCase().includes(term)
    );
  }

  goToAddContract(): void {
    this.router.navigate(['/rentals/contracts/add']);
  }

  getStatusClass(status: ContractStatus): string {
    switch (status) {
      case ContractStatus.Active: return 'badge-active';
      case ContractStatus.Closed: return 'badge-closed';
      case ContractStatus.Cancelled: return 'badge-cancelled';
      default: return 'badge-draft';
    }
  }
}

