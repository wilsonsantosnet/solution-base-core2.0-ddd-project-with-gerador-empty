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
      <#classItems#>
    };
  }

}
