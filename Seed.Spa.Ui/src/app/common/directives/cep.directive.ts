import { Directive, EventEmitter, Output } from '@angular/core';
import { NgModel, FormControlName } from '@angular/forms';

import { ApiService } from '../services/api.service';
import { GlobalService, NotificationParameters } from "../../global.service";

@Directive({
  selector: '[ngModel][cepFinder]',
  providers: [NgModel],
  host: {
    '(focusout)': 'onInputChange($event)'
  }
})
export class CepDirective {

  @Output() onfind: EventEmitter<any>;

  _endpoint: string;

  constructor(private api: ApiService<any>, private ngModel: NgModel) {
    this.onfind = new EventEmitter<any>();
    this._endpoint = "https://target-cep.azurewebsites.net/api/";
  }

  onInputChange(data) {
    var value = data.target.value;
    if (value) {
      var cep = value.replace(/\D/g, '');
      if (cep.length == 8)
        this.findCep(cep);
    }
  }

  findCep(cep) {
    this.api.setResource("log_logradouro/GetDataListCustom", this._endpoint).get({ cep: cep }).subscribe((result) => {
      if (result.DataList == null || result.DataList.length < 1)
        result.DataList = [{ bairro: null, cep: null, cidade: null, logradouro: null, tipo: null, uf: null }];
      this.onfind.emit(result.DataList[0]);
    })
  }

}
