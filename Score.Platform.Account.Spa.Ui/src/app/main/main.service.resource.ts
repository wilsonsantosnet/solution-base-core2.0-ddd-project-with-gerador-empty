import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { ServiceBase } from '../common/services/service.base';


@Injectable()
export class MainServiceResource extends ServiceBase {


  constructor() {
    super()
  }



  getInfosResources() {
    return {
            Program: { label: 'Program' },
      Tenant: { label: 'Tenant' },
      Thema: { label: 'Thema' },
    };
  }

}
