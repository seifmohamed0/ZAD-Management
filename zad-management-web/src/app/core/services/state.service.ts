import { Injectable, signal, computed, inject } from '@angular/core';
import { CompanyService } from './company.service';
import { BranchService } from './branch.service';
import { Company } from '../models/company.model';
import { Branch } from '../models/branch.model';

@Injectable({
  providedIn: 'root'
})
export class StateService {
  private companyService = inject(CompanyService);
  private branchService = inject(BranchService);

  companies = signal<Company[]>([]);
  branches = signal<Branch[]>([]);
  selectedCompanyId = signal<number | null>(null);
  selectedBranchId = signal<number | null>(null);
  isLoading = signal<boolean>(false);

  selectedCompany = computed(() =>
    this.companies().find(c => c.id === this.selectedCompanyId()) || null
  );

  selectedBranch = computed(() =>
    this.branches().find(b => b.id === this.selectedBranchId()) || null
  );

  filteredBranches = computed(() => {
    const compId = this.selectedCompanyId();
    if (!compId) return this.branches();
    return this.branches().filter(b => b.companyId === compId);
  });

  constructor() {
    this.loadInitialData();
  }

  loadInitialData(): void {
    this.isLoading.set(true);
    this.companyService.getAll().subscribe({
      next: (companies) => {
        this.companies.set(companies);
        if (companies.length > 0 && !this.selectedCompanyId()) {
          this.selectedCompanyId.set(companies[0].id);
        }
        this.loadBranches();
      },
      error: () => this.isLoading.set(false)
    });
  }

  loadBranches(): void {
    this.branchService.getAll().subscribe({
      next: (branches) => {
        this.branches.set(branches);
        const filtered = this.filteredBranches();
        if (filtered.length > 0 && !this.selectedBranchId()) {
          this.selectedBranchId.set(filtered[0].id);
        }
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  selectCompany(companyId: number): void {
    this.selectedCompanyId.set(companyId);
    const filtered = this.branches().filter(b => b.companyId === companyId);
    if (filtered.length > 0) {
      this.selectedBranchId.set(filtered[0].id);
    } else {
      this.selectedBranchId.set(null);
    }
  }

  selectBranch(branchId: number): void {
    this.selectedBranchId.set(branchId);
  }
}

