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
  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }
}

