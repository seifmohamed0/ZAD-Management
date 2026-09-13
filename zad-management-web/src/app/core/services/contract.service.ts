import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RentalContractListDto, RentalContractDto, CreateRentalContractDto, RentalCalculationResult } from '../models/contract.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ContractService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/rentals/contracts`;

  getAll(branchId?: number): Observable<RentalContractListDto[]> {
    let params = new HttpParams();
    if (branchId) {
      params = params.set('branchId', branchId.toString());
    }
    return this.http.get<RentalContractListDto[]>(this.apiUrl, { params });
  }

  getById(id: number): Observable<RentalContractDto> {
    return this.http.get<RentalContractDto>(`${this.apiUrl}/${id}`);
  }

  create(dto: CreateRentalContractDto): Observable<{ id: number; message: string }> {
    return this.http.post<{ id: number; message: string }>(this.apiUrl, dto);
  }

  closeContract(id: number, data: {
    actualReturnDate: string;
    returnKm: number;
    maintenancePenaltyAmount?: number;
    accidentPenaltyAmount?: number;
    driverAmount?: number;
    paidAmount?: number;
    exitDiscountAmount?: number;
    maintenancePaidByTenant?: number;
    maintenanceDoneByTenant?: boolean;
    notes?: string;
    pricing?: {
      rentPrice: number;
      discountPercent: number;
      discountAmount: number;
    };
    mileage?: {
      kilometerPerDay: number;
      maximumKilometerPerDay: number;
      amountOfKmExceedingLimit: number;
    };
    penalties?: {
      delayPenaltyPerHour: number;
      allowedDelayHours: number;
      maintenancePenalty: number;
      accidentPenalty: number;
      amountOfKmExceedingLimit: number;
    };
  }): Observable<RentalCalculationResult> {
    return this.http.post<RentalCalculationResult>(`${this.apiUrl}/${id}/close`, data);
  }
}

