import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { ApiService } from '../services/api.service';
import { GlobalService } from '../../global.service';
import { ViewModel } from '../model/viewmodel';

@Component({
  selector: 'cep',
  template: `<div [formGroup]="vm.form">
      <input type='text' class='form-control' [(ngModel)]='vm.model[fieldName]' name='{{fieldName}}' cepFinder (onfind)="onFindCep($event)" formControlName="{{fieldName}}" [textMask]="{mask: vm.masks.maskCEP}" required="{{isRequired}}" />      
    </div>`
})
export class CepComponent implements OnInit {

  @Input() vm: ViewModel<any>
  @Input() fieldName: string
  @Input() isRequired: boolean;
  @Output() cepChange = new EventEmitter<any>();
  
  constructor() {
  }

  ngOnInit(): void {
    if (this.vm.model.city && this.vm.model.state) {
      this.vm.model.disabledCity = true;
      this.vm.model.disabledState = true;
    }
  }

  onFindCep(data: any) {

    this.vm.model.logradouro = data.logradouro;
    this.vm.model.cidade = data.cidade;
    this.vm.model.uf = data.uf;
    this.vm.model.bairro = data.bairro;

    this.vm.model.address = data.logradouro;
    this.vm.model.city = data.cidade;
    this.vm.model.state = data.uf;
    this.vm.model.neighborhood = data.bairro;

    if (data.cidade && data.uf) {
      this.vm.model.disabledCidade = true;
      this.vm.model.disabledCity = true;
      this.vm.model.disabledUf = true;
      this.vm.model.disabledState = true;

    } else {
      this.vm.model.disabledCidade = false;
      this.vm.model.disabledCity = false;
      this.vm.model.disabledUf = false;
      this.vm.model.disabledState = false;
    }

    this.cepChange.emit(data);    
  }


}
