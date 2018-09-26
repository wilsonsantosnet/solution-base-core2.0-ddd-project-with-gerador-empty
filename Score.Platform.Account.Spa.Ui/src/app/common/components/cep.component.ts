import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { ApiService } from '../services/api.service';
import { GlobalService } from '../../global.service';
import { ViewModel } from '../model/viewmodel';

@Component({
  selector: 'cep',
  template: `<div [formGroup]="vm.form">
      <input type='text' class='form-control' [(ngModel)]='vm.model.cep' name='cep' cepFinder (onfind)="onFindCep($event)" formControlName='cep' [textMask]="{mask: vm.masks.maskCEP}" required />      
    </div>`
})
export class CepComponent implements OnInit {

  @Input() vm: ViewModel<any>
  @Output() cepChange = new EventEmitter<any>();
  
  constructor() {
  }

  ngOnInit(): void {

  }

  onFindCep(data: any) {

    this.vm.model.logradouro = data.logradouro;
    this.vm.model.cidade = data.cidade;
    this.vm.model.uf = data.uf;
    this.vm.model.bairro = data.bairro;

    this.cepChange.emit(data);    
  }


}
