export enum ContractStatus {
  Draft = 1,
  Active = 2,
  Closed = 3,
  Cancelled = 4
}

export enum ContractType {
  Daily = 1,
  Weekly = 2,
  Monthly = 3
}

export enum PaymentType {
  Cash = 1,
  CreditCard = 2,
  BankTransfer = 3,
  Deferred = 4
}

export enum NotificationType {
  Kilometer = 1,
  Date = 2,
  Both = 3
}

export interface RentalContractListDto {
  id: number;
  contractNumber: string;
  referenceNo?: string;
  companyName: string;
  branchName: string;
  tenantName: string;
  tenantMobile: string;
  vehiclePlateNo: string;
  startDate: string;
  expectedReceivingDate: string;
  periodInDays: number;
  netRentPrice: number;
  currency: string;
  status: ContractStatus;
  statusName: string;
  contractType: ContractType;
  contractTypeName: string;
}

export interface RentalContractDto {
  id: number;
  companyId: number;
  companyName?: string;
  branchId: number;
  branchName?: string;
  contractNumber: string;
  accountingNo?: string;
  referenceNo?: string;
  currency: string;
  status: ContractStatus;
  statusName: string;
  contractType: ContractType;
  contractTypeName: string;
  paymentType: PaymentType;
  paymentTypeName: string;
  withDriver: boolean;
  driverName?: string;
  notes?: string;
  createdAt: string;

  // Period
  startDate: string;
  startTime: string;
  startDay: string;
  expectedReceivingDate: string;
  expectedReceivingTime: string;
  deliveryDay: string;
  periodInDays: number;
  actualPeriodInDays: number;

  // Tenant
  tenantName: string;
  licenseNumber: string;
  passportNumber?: string;
  unifiedNumber?: string;
  idNumber: string;
  mobile: string;
  tenantBirthday?: string;
  tenantAge?: number;

  // Sponsor
  sponsorName?: string;
  sponsorNationality?: string;
  sponsorLicenseNumber?: string;
  sponsorLicenseExpireDate?: string;
  sponsorIdNumber?: string;
  sponsorIdExpireDate?: string;

  // Second Driver
  secondDriverName?: string;
  secondDriverNationality?: string;
  secondDriverLicenseNumber?: string;
  secondDriverLicenseExpireDate?: string;
  secondDriverIdNumber?: string;
  secondDriverIdExpireDate?: string;

  // Vehicle
  vehiclePlateNo: string;
  vehicleModelYear: string;
  vehicleFileNo: string;
  startKilometerCounter: number;
  returnKilometerCounter?: number;

  // Pricing
  rentPrice: number;
  discountPercent: number;
  discountAmount: number;
  netRentPrice: number;

  // Penalties
  delayPenaltyPerHour: number;
  allowedDelayHours: number;
  maintenancePenalty: number;
  accidentPenalty: number;

  // Driver Terms
  driverFare?: number;
  driverWorkingHoursPerDay?: number;
  driverOvertimeAmountPerHour?: number;
  driverDailyRate?: number;

  // Mileage
  kilometerPerDay: number;
  maximumKilometerPerDay: number;
  amountOfKmExceedingLimit: number;

  // Maintenance
  nextMaintenanceDate?: string;
  nextMaintenanceKm?: number;
  reminderBeforePeriodicMaintenance?: number;
  notificationType?: NotificationType;
}

export interface CreateRentalContractDto {
  companyId: number;
  branchId: number;
  referenceNo?: string;
  accountingNo?: string;
  currency: string;
  contractType: ContractType;
  paymentType: PaymentType;
  withDriver: boolean;
  driverName?: string;
  notes?: string;

  startDate: string;
  startTime: string;
  expectedReceivingDate: string;
  expectedReceivingTime: string;
  periodInDays?: number;

  tenant: {
    tenantName: string;
    licenseNumber: string;
    passportNumber?: string;
    unifiedNumber?: string;
    idNumber: string;
    mobile: string;
    tenantBirthday?: string;
  };

  sponsor?: {
    sponsorName?: string;
    nationality?: string;
    licenseNumber?: string;
    licenseExpireDate?: string;
    idNumber?: string;
    idExpireDate?: string;
  };

  secondDriver?: {
    secondDriverName?: string;
    nationality?: string;
    licenseNumber?: string;
    licenseExpireDate?: string;
    idNumber?: string;
    idExpireDate?: string;
  };

  vehicle: {
    plateNo: string;
    modelYear: string;
    fileNo: string;
    startKilometerCounter: number;
  };

  pricing: {
    rentPrice: number;
    discountPercent: number;
    discountAmount: number;
  };

  penalties: {
    delayPenaltyPerHour: number;
    allowedDelayHours: number;
    maintenancePenalty: number;
    accidentPenalty: number;
  };

  driverTerms?: {
    driverFare: number;
    driverWorkingHoursPerDay: number;
    driverOvertimeAmountPerHour: number;
    dailyRate: number;
  };

  mileage: {
    kilometerPerDay: number;
    maximumKilometerPerDay: number;
    amountOfKmExceedingLimit: number;
  };

  maintenance?: {
    nextMaintenanceDate?: string;
    nextMaintenanceKm?: number;
    reminderBeforePeriodicMaintenance?: number;
    notificationType?: NotificationType;
  };
}

