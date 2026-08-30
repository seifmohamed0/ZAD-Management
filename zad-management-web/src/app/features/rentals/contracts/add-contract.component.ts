import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterModule } from '@angular/router';
import { ContractService } from '../../../core/services/contract.service';
import { StateService } from '../../../core/services/state.service';
import { ContractType, PaymentType, NotificationType, CreateRentalContractDto } from '../../../core/models/contract.model';

@Component({
  selector: 'app-add-contract',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule, RouterModule],
  templateUrl: './add-contract.component.html',
  styleUrls: ['./add-contract.component.scss']
})
export class AddContractComponent implements OnInit {
  private fb = inject(FormBuilder);
  private contractService = inject(ContractService);
  public state = inject(StateService);
  private router = inject(Router);

  activeTab = signal<'tenant' | 'vehicle' | 'diagram' | 'documents'>('tenant');
  isSubmitting = signal<boolean>(false);
  errorMessage = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  form!: FormGroup;

  daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

  ContractType = ContractType;
  PaymentType = PaymentType;
  NotificationType = NotificationType;

  // Car Diagram Checkpoints
  diagramPoints = [
    { id: 'f-bumper', label: 'Front Bumper', checked: false },
    { id: 'r-bumper', label: 'Rear Bumper', checked: false },
    { id: 'l-door', label: 'Left Doors', checked: false },
    { id: 'r-door', label: 'Right Doors', checked: false },
    { id: 'hood', label: 'Hood / Engine', checked: false },
    { id: 'windshield', label: 'Windshield Glass', checked: false },
    { id: 'tires', label: 'Tires & Rims', checked: false },
    { id: 'interior', label: 'Interior Seats & Dash', checked: false }
  ];

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    const today = new Date();
    const threeDaysLater = new Date(today);
    threeDaysLater.setDate(today.getDate() + 3);

    const defaultBranchId = this.state.selectedBranchId() || null;
    const defaultCompanyId = this.state.selectedCompanyId() || null;

    this.form = this.fb.group({
      // Settings Header
      companyId: [defaultCompanyId, Validators.required],
      branchId: [defaultBranchId, Validators.required],
      time: ['09:00'],
      date: [this.formatDate(today), Validators.required],
      day: [this.daysOfWeek[today.getDay()]],
      accountingNo: ['ACC-' + Math.floor(1000 + Math.random() * 9000)],
      referenceNo: ['REF-' + Math.floor(1000 + Math.random() * 9000)],
      currency: ['SAR', Validators.required],
      status: ['New'],
      contractType: [ContractType.Daily, Validators.required],
      paymentType: [PaymentType.Cash, Validators.required],
      periodInDays: [3, [Validators.required, Validators.min(1)]],
      actualPeriodInDays: [0],
      expectedReceivingTime: ['09:00', Validators.required],
      expectedReceivingDate: [this.formatDate(threeDaysLater), Validators.required],
      deliveryDay: [this.daysOfWeek[threeDaysLater.getDay()]],
      withDriver: [false],
      driverName: [''],
      notes: [''],

      // Tab 1: Tenant
      tenant: this.fb.group({
        tenantName: ['', Validators.required],
        licenseNumber: ['', Validators.required],
        passportNumber: [''],
        unifiedNumber: [''],
        idNumber: ['', Validators.required],
        mobile: ['', Validators.required],
        tenantBirthday: [''],
        tenantAge: ['']
      }),

      sponsor: this.fb.group({
        sponsorName: [''],
        nationality: ['Saudi'],
        licenseNumber: [''],
        licenseExpireDate: [''],
        idNumber: [''],
        idExpireDate: ['']
      }),

      secondDriver: this.fb.group({
        secondDriverName: [''],
        nationality: ['Saudi'],
        licenseNumber: [''],
        licenseExpireDate: [''],
        idNumber: [''],
        idExpireDate: ['']
      }),

      // Tab 2: Vehicle Info
      vehicle: this.fb.group({
        plateNo: ['', Validators.required],
        modelYear: ['2024'],
        fileNo: ['FILE-' + Math.floor(100 + Math.random() * 900)],
        startKilometerCounter: [15000, [Validators.required, Validators.min(0)]]
      }),

      pricing: this.fb.group({
        rentPrice: [200, [Validators.required, Validators.min(0)]],
        discountPercent: [0],
        discountAmount: [0],
        netRentPrice: [{ value: 200, disabled: true }]
      }),

      penalties: this.fb.group({
        delayPenaltyPerHour: [50],
        allowedDelayHours: [2],
        maintenancePenalty: [150],
        accidentPenalty: [500]
      }),

      driverTerms: this.fb.group({
        driverFare: [100],
        driverWorkingHoursPerDay: [8],
        driverOvertimeAmountPerHour: [25],
        dailyRate: [120]
      }),

      mileage: this.fb.group({
        kilometerPerDay: [200],
        maximumKilometerPerDay: [350],
        amountOfKmExceedingLimit: [1.5]
      }),

      maintenance: this.fb.group({
        nextMaintenanceDate: ['2026-10-01'],
        nextMaintenanceKm: [20000],
        reminderBeforePeriodicMaintenance: [7],
        notificationType: [NotificationType.Kilometer]
      })
    });

    this.setupLiveCalculations();
  }

  setupLiveCalculations(): void {
    // 1. Calculate Expected Date when Start Date or Period changes
    this.form.get('date')?.valueChanges.subscribe(d => this.updateDateCalculations());
    this.form.get('periodInDays')?.valueChanges.subscribe(p => this.updateDateCalculations());

    // 2. Birthday -> Age calculation
    this.form.get('tenant.tenantBirthday')?.valueChanges.subscribe(bday => {
      if (bday) {
        const birthDate = new Date(bday);
        const today = new Date();
        let age = today.getFullYear() - birthDate.getFullYear();
        const m = today.getMonth() - birthDate.getMonth();
        if (m < 0 || (m === 0 && today.getDate() < birthDate.getDate())) age--;
        this.form.get('tenant.tenantAge')?.setValue(age > 0 ? `${age} Years` : '0');
      } else {
        this.form.get('tenant.tenantAge')?.setValue('');
      }
    });

    // 3. Pricing & Discount Calculations
    this.form.get('pricing.rentPrice')?.valueChanges.subscribe(() => this.recalculatePricing(true));
    this.form.get('pricing.discountPercent')?.valueChanges.subscribe(() => this.recalculatePricing(true));
    this.form.get('pricing.discountAmount')?.valueChanges.subscribe(() => this.recalculatePricing(false));
  }

  updateDateCalculations(): void {
    const startDateStr = this.form.get('date')?.value;
    const days = parseInt(this.form.get('periodInDays')?.value, 10) || 1;

    if (startDateStr) {
      const startDate = new Date(startDateStr);
      this.form.get('day')?.setValue(this.daysOfWeek[startDate.getDay()]);

      const receivingDate = new Date(startDate);
      receivingDate.setDate(startDate.getDate() + days);
      this.form.get('expectedReceivingDate')?.setValue(this.formatDate(receivingDate), { emitEvent: false });
      this.form.get('deliveryDay')?.setValue(this.daysOfWeek[receivingDate.getDay()]);
    }
  }

  recalculatePricing(fromPercent: boolean): void {
    const rentPrice = parseFloat(this.form.get('pricing.rentPrice')?.value) || 0;

    if (fromPercent) {
      const discPercent = parseFloat(this.form.get('pricing.discountPercent')?.value) || 0;
      const discAmount = Math.round((rentPrice * (discPercent / 100)) * 100) / 100;
      this.form.get('pricing.discountAmount')?.setValue(discAmount, { emitEvent: false });
      const net = Math.max(0, rentPrice - discAmount);
      this.form.get('pricing.netRentPrice')?.setValue(net);
    } else {
      const discAmount = parseFloat(this.form.get('pricing.discountAmount')?.value) || 0;
      const discPercent = rentPrice > 0 ? Math.round(((discAmount / rentPrice) * 100) * 100) / 100 : 0;
      this.form.get('pricing.discountPercent')?.setValue(discPercent, { emitEvent: false });
      const net = Math.max(0, rentPrice - discAmount);
      this.form.get('pricing.netRentPrice')?.setValue(net);
    }
  }

  formatDate(d: Date): string {
    return d.toISOString().split('T')[0];
  }

  setTab(tab: 'tenant' | 'vehicle' | 'diagram' | 'documents'): void {
    this.activeTab.set(tab);
  }

  toggleDiagramPoint(point: any): void {
    point.checked = !point.checked;
  }

  saveContract(): void {
    this.errorMessage.set(null);
    this.successMessage.set(null);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.errorMessage.set('Please fill in all required fields (Tenant, Vehicle, Dates, Branch).');
      return;
    }

    this.isSubmitting.set(true);
    const val = this.form.getRawValue();

    const dto: CreateRentalContractDto = {
      companyId: val.companyId || this.state.selectedCompanyId() || 1,
      branchId: val.branchId || this.state.selectedBranchId() || 1,
      referenceNo: val.referenceNo,
      accountingNo: val.accountingNo,
      currency: val.currency || 'SAR',
      contractType: Number(val.contractType),
      paymentType: Number(val.paymentType),
      withDriver: val.withDriver === true || val.withDriver === 'true',
      driverName: val.driverName,
      notes: val.notes,

      startDate: new Date(`${val.date}T${val.time}:00Z`).toISOString(),
      startTime: `${val.time}:00`,
      expectedReceivingDate: new Date(`${val.expectedReceivingDate}T${val.expectedReceivingTime}:00Z`).toISOString(),
      expectedReceivingTime: `${val.expectedReceivingTime}:00`,
      periodInDays: Number(val.periodInDays),

      tenant: {
        tenantName: val.tenant.tenantName,
        licenseNumber: val.tenant.licenseNumber,
        passportNumber: val.tenant.passportNumber,
        unifiedNumber: val.tenant.unifiedNumber,
        idNumber: val.tenant.idNumber,
        mobile: val.tenant.mobile,
        tenantBirthday: val.tenant.tenantBirthday ? new Date(val.tenant.tenantBirthday).toISOString() : undefined
      },

      sponsor: val.sponsor?.sponsorName ? {
        sponsorName: val.sponsor.sponsorName,
        nationality: val.sponsor.nationality,
        licenseNumber: val.sponsor.licenseNumber,
        licenseExpireDate: val.sponsor.licenseExpireDate ? new Date(val.sponsor.licenseExpireDate).toISOString() : undefined,
        idNumber: val.sponsor.idNumber,
        idExpireDate: val.sponsor.idExpireDate ? new Date(val.sponsor.idExpireDate).toISOString() : undefined
      } : undefined,

      secondDriver: val.secondDriver?.secondDriverName ? {
        secondDriverName: val.secondDriver.secondDriverName,
        nationality: val.secondDriver.nationality,
        licenseNumber: val.secondDriver.licenseNumber,
        licenseExpireDate: val.secondDriver.licenseExpireDate ? new Date(val.secondDriver.licenseExpireDate).toISOString() : undefined,
        idNumber: val.secondDriver.idNumber,
        idExpireDate: val.secondDriver.idExpireDate ? new Date(val.secondDriver.idExpireDate).toISOString() : undefined
      } : undefined,

      vehicle: {
        plateNo: val.vehicle.plateNo,
        modelYear: val.vehicle.modelYear,
        fileNo: val.vehicle.fileNo,
        startKilometerCounter: Number(val.vehicle.startKilometerCounter)
      },

      pricing: {
        rentPrice: Number(val.pricing.rentPrice),
        discountPercent: Number(val.pricing.discountPercent),
        discountAmount: Number(val.pricing.discountAmount)
      },

      penalties: {
        delayPenaltyPerHour: Number(val.penalties.delayPenaltyPerHour),
        allowedDelayHours: Number(val.penalties.allowedDelayHours),
        maintenancePenalty: Number(val.penalties.maintenancePenalty),
        accidentPenalty: Number(val.penalties.accidentPenalty)
      },

      driverTerms: {
        driverFare: Number(val.driverTerms.driverFare),
        driverWorkingHoursPerDay: Number(val.driverTerms.driverWorkingHoursPerDay),
        driverOvertimeAmountPerHour: Number(val.driverTerms.driverOvertimeAmountPerHour),
        dailyRate: Number(val.driverTerms.dailyRate)
      },

      mileage: {
        kilometerPerDay: Number(val.mileage.kilometerPerDay),
        maximumKilometerPerDay: Number(val.mileage.maximumKilometerPerDay),
        amountOfKmExceedingLimit: Number(val.mileage.amountOfKmExceedingLimit)
      },

      maintenance: {
        nextMaintenanceDate: val.maintenance?.nextMaintenanceDate ? new Date(val.maintenance.nextMaintenanceDate).toISOString() : undefined,
        nextMaintenanceKm: Number(val.maintenance?.nextMaintenanceKm),
        reminderBeforePeriodicMaintenance: Number(val.maintenance?.reminderBeforePeriodicMaintenance),
        notificationType: Number(val.maintenance?.notificationType)
      }
    };

    this.contractService.create(dto).subscribe({
      next: (res) => {
        this.isSubmitting.set(false);
        this.successMessage.set('Contract created successfully!');
        setTimeout(() => {
          this.router.navigate(['/rentals/contracts']);
        }, 1200);
      },
      error: (err) => {
        this.isSubmitting.set(false);
        this.errorMessage.set(err?.error?.message || 'Error occurred while saving contract.');
      }
    });
  }

  cancel(): void {
    this.router.navigate(['/rentals/contracts']);
  }
}

