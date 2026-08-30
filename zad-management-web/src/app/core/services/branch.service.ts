import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Branch, CreateBranchDto, UpdateBranchDto } from '../models/branch.model';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BranchService {
  private http = inject(HttpClient);
  private apiUrl = `${environment.apiUrl}/settings/branches`;

  getAll(): Observable<Branch[]> {
    return this.http.get<Branch[]>(this.apiUrl);
  }

  getById(id: number): Observable<Branch> {
    return this.http.get<Branch>(`${this.apiUrl}/${id}`);
  }

  create(dto: CreateBranchDto): Observable<number> {
    return this.http.post<number>(this.apiUrl, dto);
  }

  update(id: number, dto: UpdateBranchDto): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, dto);
  }

  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}

