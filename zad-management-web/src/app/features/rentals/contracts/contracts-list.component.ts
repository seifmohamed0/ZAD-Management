import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { ContractService } from '../../../core/services/contract.service';
import { StateService } from '../../../core/services/state.service';
import { RentalContractListDto, ContractStatus, ContractType, RentalCalculationResult } from '../../../core/models/contract.model';

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
  searchTerm = signal<string>('');
  isLoading = signal<boolean>(false);

  // Close Contract Modal State
  isCloseModalOpen = signal<boolean>(false);
  selectedContractForClose = signal<RentalContractListDto | null>(null);
  actualReturnDate = signal<string>('');
  returnKm = signal<number>(0);
  isClosing = signal<boolean>(false);
  closeError = signal<string | null>(null);
  calculationResult = signal<RentalCalculationResult | null>(null);

  ContractStatus = ContractStatus;
  ContractType = ContractType;

  ngOnInit(): void {
    this.loadContracts();
  }

  loadContracts(): void {
    this.isLoading.set(true);
    const branchId = this.state.selectedBranchId() || undefined;
    this.contractService.getAll(branchId).subscribe({
      next: (data) => {
        this.contracts.set(data);
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  onCompanyChange(companyId: any): void {
    const id = Number(companyId) || null;
    if (id) {
      this.state.selectCompany(id);
    }
    this.loadContracts();
  }

  onBranchChange(branchId: any): void {
    const id = Number(branchId) || null;
    if (id) {
      this.state.selectBranch(id);
    } else {
      this.state.selectedBranchId.set(null);
    }
    this.loadContracts();
  }

  get filteredContracts(): RentalContractListDto[] {
    const term = this.searchTerm().toLowerCase().trim();
    if (!term) return this.contracts();
    return this.contracts().filter(c =>
      c.id.toString() === term ||
      c.id.toString().includes(term) ||
      c.contractNumber?.toLowerCase().includes(term) ||
      c.tenantName?.toLowerCase().includes(term) ||
      c.vehiclePlateNo?.toLowerCase().includes(term) ||
      c.tenantMobile?.includes(term)
    );
  }

  goToAddContract(): void {
    this.router.navigate(['/rentals/contracts/add']);
  }

  openCloseModal(contract: RentalContractListDto): void {
    this.selectedContractForClose.set(contract);
    this.closeError.set(null);
    this.calculationResult.set(null);

    const now = new Date();
    const pad = (n: number) => (n < 10 ? '0' + n : n);
    const formatted = `${now.getFullYear()}-${pad(now.getMonth() + 1)}-${pad(now.getDate())}T${pad(now.getHours())}:${pad(now.getMinutes())}`;
    this.actualReturnDate.set(formatted);
    this.returnKm.set(0);
    this.isCloseModalOpen.set(true);
  }

  closeModal(): void {
    this.isCloseModalOpen.set(false);
    this.selectedContractForClose.set(null);
    this.calculationResult.set(null);
    this.closeError.set(null);
  }

  submitCloseContract(): void {
    const contract = this.selectedContractForClose();
    if (!contract) return;

    if (!this.actualReturnDate()) {
      this.closeError.set('Please provide the actual return date and time.');
      return;
    }

    this.isClosing.set(true);
    this.closeError.set(null);

    const returnIso = new Date(this.actualReturnDate()).toISOString();
    this.contractService.closeContract(contract.id, {
      actualReturnDate: returnIso,
      returnKm: Number(this.returnKm()) || 0
    }).subscribe({
      next: (result) => {
        this.isClosing.set(false);
        this.calculationResult.set(result);
        this.loadContracts();
      },
      error: (err) => {
        this.isClosing.set(false);
        this.closeError.set(err.error?.message || 'Failed to close contract. Please try again.');
      }
    });
  }

  getStatusClass(status: ContractStatus): string {
    switch (status) {
      case ContractStatus.Active: return 'badge-active';
      case ContractStatus.Closed: return 'badge-closed';
      default: return 'badge-active';
    }
  }
}
