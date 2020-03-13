import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { ApiService } from '../services/api.service';
import { GlobalService } from '../../global.service';
import { ViewModel } from '../model/viewmodel';

@Component({
  selector: 'cpf-cnpj',
  template: `<div class='form-group' [formGroup]="vm.form">
              <div class="input-group">
                <div class="input-group-prepend">
                  <div class="input-group-text">
                    <input type="radio" [(ngModel)]='tpCTRLcpfCnpj'  [value]='1' name='tpCTRLcpfCnpj' [ngModelOptions]="{standalone: true}" (change)="onChangeTipo($event)">&nbsp;CPF ou &nbsp;
                    <input type="radio" [(ngModel)]='tpCTRLcpfCnpj' [value]='2' name='tpCTRLcpfCnpj' [ngModelOptions]="{standalone: true}" (change)="onChangeTipo($event)">&nbsp;CNPJ?
                  </div>
                </div>
                <input *ngIf='tpCTRLcpfCnpj==1 || !tpCTRLcpfCnpj' type='text' class='form-control' [(ngModel)]='vm.model[fieldName]' name="{{fieldName}}" formControlName="{{fieldName}}" [textMask]='{mask: vm.masks.maskCPF}' required="{isRequired}" (change)="onChangeCpf($event)" />
                <input *ngIf='tpCTRLcpfCnpj==2' type='text' class='form-control' [(ngModel)]='vm.model[fieldName]' name="{{fieldName}}" formControlName="{{fieldName}}" [textMask]='{mask: vm.masks.maskCNPJ}' required="{isRequired}" (change)="onChangeCnpj($event)"/>
              </div>
            </div>`
})
export class CpfCnpjComponent implements OnInit {

  @Input() vm: ViewModel<any>
  @Input() fieldName: string;
  @Input() isRequired: boolean;

  tpCTRLcpfCnpj: number;
  cpf: string;
  cnpj: string;

  constructor() {
  }

  ngOnInit(): void {
    this.tpCTRLcpfCnpj = 1;
  }

  onChangeTipo(e: any) {
    if (this.tpCTRLcpfCnpj == 1)
      this.vm.model[this.fieldName] = this.cpf;
    else {
      this.vm.model[this.fieldName] = this.cnpj;
    }
  }

  onChangeCpf(e: any) {
    this.cpf = this.vm.model[this.fieldName];
  }

  onChangeCnpj(e: any) {
    this.cnpj = this.vm.model[this.fieldName];
  }


}
