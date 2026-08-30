import { Component, OnInit, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule, Router } from '@angular/router';
import { ContractService } from '../../../core/services/contract.service';
import { StateService } from '../../../core/services/state.service';
import { RentalContractListDto, ContractStatus, ContractType } from '../../../core/models/contract.model';

@Component({
  selector: 'app-contracts-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterModule],
  templateUrl: './contracts-list.component.html',
  styleUrls: ['./contracts-list.component.scss']
})
export class ContractsListComponent implements OnInit {
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}

