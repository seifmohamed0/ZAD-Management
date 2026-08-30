import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { CompanyService } from '../../../core/services/company.service';
import { StateService } from '../../../core/services/state.service';
import { Company, CreateCompanyDto } from '../../../core/models/company.model';

@Component({
  selector: 'app-companies-list',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './companies-list.component.html',
  styleUrls: ['./companies-list.component.scss']
})
export class CompaniesListComponent implements OnInit {
  private companyService = inject(CompanyService);
  private state = inject(StateService);
  private fb = inject(FormBuilder);

  companies = signal<Company[]>([]);
  searchTerm = signal<string>('');
  isLoading = signal<boolean>(false);

  isModalOpen = signal<boolean>(false);
  isEditing = signal<boolean>(false);
  isSubmitting = signal<boolean>(false);
  modalError = signal<string | null>(null);
  editingId: number | null = null;
  companyForm!: FormGroup;

  ngOnInit(): void {
    this.initForm();
    this.loadCompanies();
  }

  initForm(): void {
    this.companyForm = this.fb.group({
      code: ['', [Validators.required, Validators.maxLength(50)]],
      arabicName: ['', [Validators.required, Validators.maxLength(200)]],
      englishName: ['', [Validators.required, Validators.maxLength(200)]],
      arabicAddress: [''],
      englishAddress: [''],
      country: ['EGYPT'],
      city: ['CAIRO'],
      language: ['ar'],
      phone: [''],
      website: [''],
      logo: ['']
    });
  }

  loadCompanies(): void {
    this.isLoading.set(true);
    this.companyService.getAll().subscribe({
      next: (data) => {
        this.companies.set(data);
        this.isLoading.set(false);
      },
      error: () => this.isLoading.set(false)
    });
  }

  get filteredCompanies(): Company[] {
    const term = this.searchTerm().toLowerCase().trim();
    if (!term) return this.companies();
    return this.companies().filter(c =>
      c.code?.toLowerCase().includes(term) ||
      c.englishName?.toLowerCase().includes(term) ||
      c.arabicName?.toLowerCase().includes(term) ||
      c.city?.toLowerCase().includes(term)
    );
  }

  openCreateModal(): void {
    this.isEditing.set(false);
    this.editingId = null;
    this.modalError.set(null);
    this.companyForm.reset({
      code: '',
      arabicName: '',
      englishName: '',
      arabicAddress: '',
      englishAddress: '',
      country: 'Egypt',
      city: 'Cairo',
      language: 'ar',
      phone: '',
      website: '',
      logo: ''
    });
    this.isModalOpen.set(true);
  }

  openEditModal(company: Company): void {
    this.isEditing.set(true);
    this.editingId = company.id;
    this.modalError.set(null);
    this.companyForm.patchValue({
      code: company.code || '',
      arabicName: company.arabicName || '',
      englishName: company.englishName || '',
      arabicAddress: company.arabicAddress || '',
      englishAddress: company.englishAddress || '',
      country: company.country || 'EGYPT',
      city: company.city || 'CAIRO',
      language: company.language || 'ar',
      phone: company.phone || '',
      website: company.website || '',
      logo: company.logo || ''
    });
    this.isModalOpen.set(true);
  }

  closeModal(): void {
    this.isModalOpen.set(false);
    this.modalError.set(null);
  }

  saveCompany(): void {
    this.modalError.set(null);
    if (this.companyForm.invalid) {
      this.companyForm.markAllAsTouched();
      this.modalError.set('Please fill in all required fields (Code, English Name, Arabic Name).');
      return;
    }

    const formVal = this.companyForm.value;
    const dto: CreateCompanyDto = {
      code: formVal.code?.trim() || '',
      arabicName: formVal.arabicName?.trim() || '',
      englishName: formVal.englishName?.trim() || '',
      arabicAddress: formVal.arabicAddress?.trim() || '',
      englishAddress: formVal.englishAddress?.trim() || '',
      country: formVal.country?.trim() || 'Saudi Arabia',
      city: formVal.city?.trim() || 'Riyadh',
      language: formVal.language?.trim() || 'ar',
      phone: formVal.phone?.trim() || '',
      website: formVal.website?.trim() || '',
      logo: formVal.logo?.trim() || ''
    };

    this.isSubmitting.set(true);

    if (this.isEditing() && this.editingId) {
      this.companyService.update(this.editingId, { ...dto, isActive: true }).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.loadCompanies();
          this.state.loadInitialData();
          this.closeModal();
        },
        error: (err) => {
          this.isSubmitting.set(false);
          this.modalError.set(err?.error?.message || err?.message || 'Failed to update company.');
        }
      });
    } else {
      this.companyService.create(dto).subscribe({
        next: () => {
          this.isSubmitting.set(false);
          this.loadCompanies();
          this.state.loadInitialData();
          this.closeModal();
        },
        error: (err) => {
          this.isSubmitting.set(false);
          this.modalError.set(err?.error?.message || err?.message || 'Failed to create company.');
        }
      });
    }
  }

  toggleActive(company: Company): void {
    this.companyService.update(company.id, {
      code: company.code,
      arabicName: company.arabicName,
      englishName: company.englishName,
      arabicAddress: company.arabicAddress,
      englishAddress: company.englishAddress,
      country: company.country,
      city: company.city,
      language: company.language,
      phone: company.phone,
      website: company.website,
      logo: company.logo,
      isActive: !company.isActive
    }).subscribe({
      next: () => this.loadCompanies()
    });
  }

  deleteCompany(id: number): void {
    if (confirm('Are you sure you want to delete this company?')) {
      this.companyService.delete(id).subscribe({
        next: () => {
          this.loadCompanies();
          this.state.loadInitialData();
        }
      });
    }
  }
}

