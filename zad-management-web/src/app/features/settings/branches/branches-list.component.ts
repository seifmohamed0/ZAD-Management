import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { BranchService } from '../../../core/services/branch.service';
import { CompanyService } from '../../../core/services/company.service';
import { StateService } from '../../../core/services/state.service';
import { Branch, CreateBranchDto } from '../../../core/models/branch.model';
import { Company } from '../../../core/models/company.model';

@Component({
  selector: 'app-branches-list',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './branches-list.component.html',
  styleUrls: ['./branches-list.component.scss']
})
export class BranchesListComponent implements OnInit {
  private branchService = inject(BranchService);
  private companyService = inject(CompanyService);
  public state = inject(StateService);
  private fb = inject(FormBuilder);

  branches = signal<Branch[]>([]);
  companies = signal<Company[]>([]);
  selectedCompanyFilter = signal<number | null>(null);
  searchTerm = signal<string>('');
  isLoading = signal<boolean>(false);

  isModalOpen = signal<boolean>(false);
  isEditing = signal<boolean>(false);
  isSubmitting = signal<boolean>(false);
  modalError = signal<string | null>(null);
  editingId: number | null = null;
  branchForm!: FormGroup;

  ngOnInit(): void {
    this.initForm();
    this.loadCompanies();
    this.loadBranches();
  }

  initForm(): void {
    this.branchForm = this.fb.group({
      companyId: [null, [Validators.required]],
      code: ['', [Validators.required, Validators.maxLength(50)]],
      arabicName: ['', [Validators.required, Validators.maxLength(200)]],
      englishName: ['', [Validators.required, Validators.maxLength(200)]],
      arabicAddress: [''],
      englishAddress: [''],
      phone: [''],
      logo: ['']
    });
  }

  loadCompanies(): void {
    this.companyService.getAll().subscribe({
      next: (data) => this.companies.set(data)
    });
  }

  loadBranches(): void {
    this.isLoading.set(true);
    this.branchService.getAll().subscribe({
      next: (data) => {
        this.branches.set(data);
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  get filteredBranches(): Branch[] {
    let result = this.branches();

    const companyId = this.selectedCompanyFilter();
    if (companyId) {
      result = result.filter(b => b.companyId === companyId);
    }

    const term = this.searchTerm().toLowerCase().trim();
    if (term) {
      result = result.filter(b =>
        b.code?.toLowerCase().includes(term) ||
        b.englishName?.toLowerCase().includes(term) ||
        b.arabicName?.toLowerCase().includes(term) ||
        b.phone?.toLowerCase().includes(term)
      );
    }

    return result;
  }

  getCompanyName(companyId: number): string {
    const comp = this.companies().find(c => c.id === companyId);
    return comp ? (comp.englishName || comp.arabicName) : `Company #${companyId}`;
  }

  openCreateModal(): void {
    this.isEditing.set(false);
    this.editingId = null;
    this.modalError.set(null);
    const defaultCompanyId = this.state.selectedCompanyId() || (this.companies().length > 0 ? this.companies()[0].id : null);
    this.branchForm.reset({
      companyId: defaultCompanyId,
      code: '',
      arabicName: '',
      englishName: '',
      arabicAddress: '',
      englishAddress: '',
      phone: '',
      logo: ''
    });
    this.isModalOpen.set(true);
  }

  openEditModal(branch: Branch): void {
    this.isEditing.set(true);
    this.editingId = branch.id;
    this.modalError.set(null);
    this.branchForm.patchValue({
      companyId: branch.companyId,
      code: branch.code || '',
      arabicName: branch.arabicName || '',
      englishName: branch.englishName || '',
      arabicAddress: branch.arabicAddress || '',
      englishAddress: branch.englishAddress || '',
      phone: branch.phone || '',
      logo: branch.logo || ''
    });
    this.isModalOpen.set(true);
  }

  closeModal(): void {
    this.isModalOpen.set(false);
    this.modalError.set(null);
  }

  saveBranch(): void {
    this.modalError.set(null);
    if (this.branchForm.invalid) {
      this.branchForm.markAllAsTouched();
      this.modalError.set('Please select a company and fill in Code, English Name, Arabic Name.');
      return;
    }

    const formVal = this.branchForm.value;
    const dto: CreateBranchDto = {
      companyId: Number(formVal.companyId),
      code: formVal.code?.trim() || '',
      arabicName: formVal.arabicName?.trim() || '',
      englishName: formVal.englishName?.trim() || '',
      arabicAddress: formVal.arabicAddress?.trim() || '',
      englishAddress: formVal.englishAddress?.trim() || '',
      phone: formVal.phone?.trim() || '',
      logo: formVal.logo?.trim() || ''
    };

    this.isSubmitting.set(true);

    if (this.isEditing() && this.editingId) {
      this.branchService.update(this.editingId, { ...dto, isActive: true }).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.loadBranches();
          this.state.loadBranches();
          this.closeModal();
        },
        error: (err) => {
          this.isSubmitting.set(false);
          this.modalError.set(err?.error?.message || err?.message || 'Failed to update branch.');
        }
      });
    } else {
      this.branchService.create(dto).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.loadBranches();
          this.state.loadBranches();
          this.closeModal();
        },
        error: (err) => {
          this.isSubmitting.set(false);
          this.modalError.set(err?.error?.message || err?.message || 'Failed to create branch.');
        }
      });
    }
  }

  toggleActive(branch: Branch): void {
    this.branchService.update(branch.id, {
      companyId: branch.companyId,
      code: branch.code,
      arabicName: branch.arabicName,
      englishName: branch.englishName,
      arabicAddress: branch.arabicAddress,
      englishAddress: branch.englishAddress,
      phone: branch.phone,
      logo: branch.logo,
      isActive: !branch.isActive
    }).subscribe({
      next: () => this.loadBranches()
    });
  }

  deleteBranch(id: number): void {
    if (confirm('Are you sure you want to delete this branch?')) {
      this.branchService.delete(id).subscribe({
        next: () => {
          this.loadBranches();
          this.state.loadBranches();
        }
      });
    }
  }
}

