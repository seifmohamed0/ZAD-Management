import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { ContractService } from '../../../core/services/contract.service';
import { StateService } from '../../../core/services/state.service';
import { ContractType, PaymentType, NotificationType, CreateRentalContractDto, RentalContractDto, RentalCalculationResult, ContractStatus } from '../../../core/models/contract.model';

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
  private route = inject(ActivatedRoute);

  activeTab = signal<'tenant' | 'vehicle' | 'close'>('tenant');
  contractId = signal<number | null>(null);
  existingContract = signal<RentalContractDto | null>(null);
  isLoadingContract = signal<boolean>(false);
  closeResult = signal<RentalCalculationResult | null>(null);
  closeDate = '';
  closeKm = 0;
  maintenancePenaltyAmount = 0;
  accidentPenaltyAmount = 0;
  driverAmount = 0;
  paidAmount = 0;
  exitDiscountAmount = 0;
  maintenancePaidByTenant = 0;
  maintenanceDoneByTenant = false;
  closeNotes = '';
  isSubmitting = signal<boolean>(false);
  errorMessage = signal<string | null>(null);
  successMessage = signal<string | null>(null);

  form!: FormGroup;

  daysOfWeek = ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'];

  ContractType = ContractType;
  PaymentType = PaymentType;
  NotificationType = NotificationType;

  dummyTenants = [
    {
      name: 'سيف الدين',
      license: 'LIC-998822',
      idNumber: '1088776655',
      mobile: '01012345678'
    },
    {
      name: 'أحمد محمد',
      license: 'LIC-334455',
      idNumber: '1044556677',
      mobile: '01098765432'
    },
    {
      name: 'محمد احمد',
      license: 'LIC-771122',
      idNumber: '1033221100',
      mobile: '01099887766'
    }
  ];

  dummyDrivers = [
    {
      name: 'احمد وائل',
      nationality: 'Egyptian',
      license: 'LIC-554433',
      idNumber: '1077665544'
    },
    {
      name: 'عمر خالد',
      nationality: 'Egyptian',
      license: 'LIC-112233',
      idNumber: '2011223344'
    },
    {
      name: 'سعيد سعد',
      nationality: 'Egyptian',
      license: 'LIC-889900',
      idNumber: '7841990123'
    }
  ];

  dummyVehicles = [
    {
      label: 'تويوتا كامري 2024 (سيدان)',
      plateNo: 'أ ب ج 1234',
      modelYear: '2024',
      km: 24500,
      rentPrice: 250
    },
    {
      label: 'هيونداي إلنترا 2023 (اقتصادي)',
      plateNo: 'س ص ع 5678',
      modelYear: '2023',
      km: 38000,
      rentPrice: 180
    },
    {
      label: 'مرسيدس E200 2025 (فخمة)',
      plateNo: 'د هـ و 9999',
      modelYear: '2025',
      km: 5200,
      rentPrice: 600
    },
    {
      label: 'كيا سبورتاج 2024 (SUV)',
      plateNo: 'ر ز ط 3344',
      modelYear: '2024',
      km: 19000,
      rentPrice: 320
    }
  ];

  ngOnInit(): void {
    this.initForm();
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (id) {
      this.contractId.set(id);
      this.loadContract(id);
    }
  }

  initForm(): void {
    const today = new Date();
    const threeDaysLater = new Date(today);
    threeDaysLater.setDate(today.getDate() + 3);

    const defaultBranchId = this.state.selectedBranchId() || (this.state.branches()[0]?.id ?? null);
    const defaultCompanyId = this.state.selectedCompanyId() || (this.state.companies()[0]?.id ?? null);

    this.form = this.fb.group({
      companyId: [defaultCompanyId, Validators.required],
      branchId: [defaultBranchId, Validators.required],
      time: ['09:00'],
      date: [this.formatDate(today), Validators.required],
      day: [this.daysOfWeek[today.getDay()]],
      accountingNo: ['ACC-' + Math.floor(1000 + Math.random() * 9000)],
      referenceNo: ['REF-' + Math.floor(1000 + Math.random() * 9000)],
      currency: ['SAR', Validators.required],
      status: ['Active'],
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

      // Tenant
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

      driver: this.fb.group({
        driverName: [''],
        nationality: ['Saudi'],
        licenseNumber: [''],
        licenseExpireDate: [''],
        idNumber: [''],
        idExpireDate: ['']
      }),

      driverTerms: this.fb.group({
        driverFare: [0],
        driverWorkingHoursPerDay: [0],
        driverOvertimeAmountPerHour: [0],
        dailyRate: [0]
      }),

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

      mileage: this.fb.group({
        kilometerPerDay: [0, [Validators.min(0)]],
        maximumKilometerPerDay: [0, [Validators.min(0)]],
        amountOfKmExceedingLimit: [0, [Validators.min(0)]]
      }),

      maintenance: this.fb.group({
        nextMaintenanceDate: [''],
        nextMaintenanceKm: [null, [Validators.min(0)]],
        reminderBeforePeriodicMaintenance: [7, [Validators.min(0)]],
        notificationType: [NotificationType.Kilometer]
      })
    });

    this.setupLiveCalculations();
  }

  setupLiveCalculations(): void {
    this.form.get('date')?.valueChanges.subscribe(() => this.updateDateCalculations());
    this.form.get('periodInDays')?.valueChanges.subscribe(() => this.updateDateCalculations());

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

  selectDummyTenant(index: any): void {
    const t = this.dummyTenants[Number(index)];
    if (!t) return;
    this.form.get('tenant')?.patchValue({
      tenantName: t.name,
      licenseNumber: t.license,
      idNumber: t.idNumber,
      mobile: t.mobile
    });
  }

  selectDummyDriver(index: any): void {
    const d = this.dummyDrivers[Number(index)];
    if (!d) return;
    this.form.get('driver')?.patchValue({
      driverName: d.name,
      nationality: d.nationality,
      licenseNumber: d.license,
      idNumber: d.idNumber
    });
    this.form.patchValue({ withDriver: true, driverName: d.name });
  }

  selectDummyVehicle(index: any): void {
    const v = this.dummyVehicles[Number(index)];
    if (!v) return;
    this.form.get('vehicle')?.patchValue({
      plateNo: v.plateNo,
      modelYear: v.modelYear,
      startKilometerCounter: v.km
    });
    this.form.get('pricing')?.patchValue({
      rentPrice: v.rentPrice,
      discountPercent: 0,
      discountAmount: 0,
      netRentPrice: v.rentPrice
    });
  }

  quickFillAll(): void {
    this.selectDummyTenant(0);
    this.selectDummyDriver(0);
    this.selectDummyVehicle(0);
    this.successMessage.set('Filled form with sample data! You can adjust dates or save.');
    setTimeout(() => this.successMessage.set(null), 3000);
  }

  formatDate(d: Date): string {
    return d.toISOString().split('T')[0];
  }

  setTab(tab: 'tenant' | 'vehicle' | 'close'): void {
    this.activeTab.set(tab);
  }

  isExisting(): boolean {
    return this.contractId() !== null;
  }

  isClosed(): boolean {
    const status = this.existingContract()?.status;
    return status !== undefined && status !== ContractStatus.Active;
  }

  loadContract(id: number): void {
    this.isLoadingContract.set(true);
    this.contractService.getById(id).subscribe({
      next: (contract) => {
        this.existingContract.set(contract);
        this.form.patchValue({
          companyId: contract.companyId,
          branchId: contract.branchId,
          contractType: contract.contractType,
          status: contract.statusName,
          date: contract.startDate.substring(0, 10),
          time: contract.startTime.substring(0, 5),
          periodInDays: contract.periodInDays,
          expectedReceivingDate: contract.expectedReceivingDate.substring(0, 10),
          expectedReceivingTime: contract.expectedReceivingTime.substring(0, 5),
          currency: contract.currency,
          paymentType: contract.paymentType,
          withDriver: contract.withDriver,
          driverName: contract.driverName || '',
          tenant: { tenantName: contract.tenantName, licenseNumber: contract.licenseNumber, idNumber: contract.idNumber, mobile: contract.mobile },
          driver: { driverName: contract.secondDriverName || '', nationality: contract.secondDriverNationality || 'Saudi', licenseNumber: contract.secondDriverLicenseNumber || '', idNumber: contract.secondDriverIdNumber || '' },
          vehicle: { plateNo: contract.vehiclePlateNo, modelYear: contract.vehicleModelYear, fileNo: contract.vehicleFileNo, startKilometerCounter: contract.startKilometerCounter },
          pricing: { rentPrice: contract.rentPrice, discountPercent: contract.discountPercent, discountAmount: contract.discountAmount, netRentPrice: contract.netRentPrice },
          penalties: { delayPenaltyPerHour: contract.delayPenaltyPerHour, allowedDelayHours: contract.allowedDelayHours, maintenancePenalty: contract.maintenancePenalty, accidentPenalty: contract.accidentPenalty },
          mileage: { kilometerPerDay: contract.kilometerPerDay, maximumKilometerPerDay: contract.maximumKilometerPerDay, amountOfKmExceedingLimit: contract.amountOfKmExceedingLimit },
          maintenance: { nextMaintenanceDate: contract.nextMaintenanceDate?.substring(0, 10) || '', nextMaintenanceKm: contract.nextMaintenanceKm ?? null, reminderBeforePeriodicMaintenance: contract.reminderBeforePeriodicMaintenance ?? 7, notificationType: contract.notificationType ?? NotificationType.Kilometer }
        }, { emitEvent: false });
        if (contract.actualReturnDate) {
          this.closeDate = contract.actualReturnDate.substring(0, 16);
          this.closeKm = contract.returnKilometerCounter || contract.startKilometerCounter;
          this.maintenancePenaltyAmount = contract.maintenancePenaltyAmount || 0;
          this.accidentPenaltyAmount = contract.accidentPenaltyAmount || 0;
          this.driverAmount = contract.driverAmount || 0;
          this.paidAmount = contract.paidAmount || 0;
          this.closeNotes = contract.closingNotes || '';
          this.closeResult.set(this.toCalculationResult(contract));
        } else {
          this.closeKm = contract.startKilometerCounter;
          this.closeDate = this.toDateTimeLocal(new Date());
          this.refreshClosePreview();
        }
        this.isLoadingContract.set(false);
      },
      error: (err) => {
        this.isLoadingContract.set(false);
        this.errorMessage.set(err?.error?.message || 'Unable to load contract.');
      }
    });
  }

  toDateTimeLocal(date: Date): string {
    const pad = (value: number) => value.toString().padStart(2, '0');
    return `${date.getFullYear()}-${pad(date.getMonth() + 1)}-${pad(date.getDate())}T${pad(date.getHours())}:${pad(date.getMinutes())}`;
  }

  toCalculationResult(contract: RentalContractDto): RentalCalculationResult {
    return {
      baseRent: contract.totalAmount || 0, discountAmount: contract.discountAmount, delayPenalty: contract.delayHours || 0,
      totalAmount: contract.totalAmount || 0, actualPeriodInDays: contract.actualPeriodInDays, delayHours: contract.delayHours || 0,
      totalConsumptionKilometers: contract.totalConsumptionKilometers || 0, freeKilometers: contract.freeKilometers || 0,
      exceededKilometers: contract.exceededKilometers || 0, exceededKilometersAmount: contract.exceededKilometersAmount || 0,
      maintenancePenaltyAmount: contract.maintenancePenaltyAmount || 0, accidentPenaltyAmount: contract.accidentPenaltyAmount || 0,
      driverAmount: contract.driverAmount || 0, paidAmount: contract.paidAmount || 0, netDueAmount: contract.netDueAmount || 0,
      exitDiscountAmount: contract.exitDiscountAmount || 0, maintenancePaidByTenant: contract.maintenancePaidByTenant || 0,
      maintenanceDoneByTenant: contract.maintenanceDoneByTenant || false
    };
  }

  closeExistingContract(): void {
    const id = this.contractId();
    if (!id || this.isClosed() || !this.closeDate) return;
    this.isSubmitting.set(true);
    this.errorMessage.set(null);
    this.contractService.closeContract(id, {
      actualReturnDate: new Date(this.closeDate).toISOString(), returnKm: Number(this.closeKm),
      maintenancePenaltyAmount: Number(this.maintenancePenaltyAmount) || 0,
      accidentPenaltyAmount: Number(this.accidentPenaltyAmount) || 0,
      driverAmount: Number(this.driverAmount) || 0, paidAmount: Number(this.paidAmount) || 0,
      exitDiscountAmount: Number(this.exitDiscountAmount) || 0,
      maintenancePaidByTenant: Number(this.maintenancePaidByTenant) || 0,
      maintenanceDoneByTenant: this.maintenanceDoneByTenant,
      notes: this.closeNotes
    }).subscribe({
      next: (result) => { this.closeResult.set(result); this.isSubmitting.set(false); this.loadContract(id); },
      error: (err) => { this.isSubmitting.set(false); this.errorMessage.set(err?.error?.message || 'Unable to close contract.'); }
    });
  }

  refreshClosePreview(): void {
    const contract = this.existingContract();
    if (!contract || this.isClosed() || !this.closeDate) return;
    const actualDate = new Date(this.closeDate);
    const start = new Date(`${contract.startDate.substring(0, 10)}T${contract.startTime.substring(0, 5)}`);
    const days = Math.max(1, Math.ceil((actualDate.getTime() - start.getTime()) / 86400000));
    const periods = contract.contractType === ContractType.Weekly ? Math.ceil(days / 7) : contract.contractType === ContractType.Monthly ? Math.ceil(days / 30) : contract.contractType === ContractType.Hourly ? Math.max(1, Math.ceil((actualDate.getTime() - start.getTime()) / 3600000)) : days;
    const delayStart = new Date(`${contract.expectedReceivingDate.substring(0, 10)}T${contract.expectedReceivingTime.substring(0, 5)}`);
    const delayHours = Math.max(0, Math.ceil((actualDate.getTime() - delayStart.getTime()) / 3600000) - Number(contract.allowedDelayHours));
    const consumption = Math.max(0, Number(this.closeKm) - contract.startKilometerCounter);
    const free = contract.maximumKilometerPerDay * days;
    const exceeded = Math.max(0, consumption - free);
    const delayPenalty = delayHours * contract.delayPenaltyPerHour;
    const maintenanceAmount = this.maintenanceDoneByTenant ? 0 : Number(this.maintenancePenaltyAmount);
    const total = periods * contract.rentPrice - periods * contract.discountAmount + delayPenalty + exceeded * contract.amountOfKmExceedingLimit + maintenanceAmount + Number(this.accidentPenaltyAmount) + Number(this.driverAmount) - Number(this.maintenancePaidByTenant);
    this.closeResult.set({
      baseRent: periods * contract.rentPrice, discountAmount: periods * contract.discountAmount, delayPenalty, totalAmount: total,
      actualPeriodInDays: days, delayHours, totalConsumptionKilometers: consumption, freeKilometers: free, exceededKilometers: exceeded,
      exceededKilometersAmount: exceeded * contract.amountOfKmExceedingLimit, maintenancePenaltyAmount: maintenanceAmount, accidentPenaltyAmount: Number(this.accidentPenaltyAmount),
      driverAmount: Number(this.driverAmount), paidAmount: Number(this.paidAmount), netDueAmount: Math.max(0, total - Number(this.paidAmount) - Number(this.exitDiscountAmount)),
      exitDiscountAmount: Number(this.exitDiscountAmount), maintenancePaidByTenant: Number(this.maintenancePaidByTenant),
      maintenanceDoneByTenant: this.maintenanceDoneByTenant
    });
  }

  saveContract(): void {
    this.errorMessage.set(null);
    this.successMessage.set(null);

    if (this.form.invalid) {
      this.form.markAllAsTouched();
      this.errorMessage.set('Please fill in required fields (Tenant name, license, ID, mobile, and vehicle plate).');
      return;
    }

    this.isSubmitting.set(true);
    const val = this.form.getRawValue();

    const dto: CreateRentalContractDto = {
      companyId: Number(val.companyId) || this.state.selectedCompanyId() || 1,
      branchId: Number(val.branchId) || this.state.selectedBranchId() || 1,
      referenceNo: val.referenceNo,
      accountingNo: val.accountingNo,
      currency: val.currency || 'SAR',
      contractType: Number(val.contractType),
      paymentType: Number(val.paymentType),
      withDriver: val.withDriver === true || val.withDriver === 'true',
      driverName: val.driver?.driverName || val.driverName,
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

      secondDriver: val.driver?.driverName ? {
        secondDriverName: val.driver.driverName,
        nationality: val.driver.nationality,
        licenseNumber: val.driver.licenseNumber,
        licenseExpireDate: val.driver.licenseExpireDate ? new Date(val.driver.licenseExpireDate).toISOString() : undefined,
        idNumber: val.driver.idNumber,
        idExpireDate: val.driver.idExpireDate ? new Date(val.driver.idExpireDate).toISOString() : undefined
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

      driverTerms: val.withDriver ? {
        driverFare: Number(val.driverTerms.driverFare),
        driverWorkingHoursPerDay: Number(val.driverTerms.driverWorkingHoursPerDay),
        driverOvertimeAmountPerHour: Number(val.driverTerms.driverOvertimeAmountPerHour),
        dailyRate: Number(val.driverTerms.dailyRate)
      } : undefined,

      mileage: {
        kilometerPerDay: Number(val.mileage.kilometerPerDay),
        maximumKilometerPerDay: Number(val.mileage.maximumKilometerPerDay),
        amountOfKmExceedingLimit: Number(val.mileage.amountOfKmExceedingLimit)
      },

      maintenance: {
        nextMaintenanceDate: val.maintenance.nextMaintenanceDate ? new Date(val.maintenance.nextMaintenanceDate).toISOString() : undefined,
        nextMaintenanceKm: val.maintenance.nextMaintenanceKm === null || val.maintenance.nextMaintenanceKm === '' ? undefined : Number(val.maintenance.nextMaintenanceKm),
        reminderBeforePeriodicMaintenance: Number(val.maintenance.reminderBeforePeriodicMaintenance),
        notificationType: Number(val.maintenance.notificationType)
      }
    };

    this.contractService.create(dto).subscribe({
      next: () => {
        this.isSubmitting.set(false);
        this.successMessage.set('Contract created and activated successfully!');
        setTimeout(() => {
          this.router.navigate(['/rentals/contracts']);
        }, 1000);
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
