import { Injectable, inject } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { RentalContractListDto, RentalContractDto, CreateRentalContractDto } from '../models/contract.model';
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
}

