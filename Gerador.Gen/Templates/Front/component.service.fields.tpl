import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { ServiceBase } from '../../common/services/service.base';

@Injectable()
export class <#className#>ServiceFields extends ServiceBase {


    constructor() {
		super()
	}

	getFormFields(moreFormControls? : any) {
		var formControls = Object.assign(moreFormControls || {},{
<#riquered#>
        });
		return new FormGroup(formControls);
	}



	getInfosFields(moreInfosFields? : any, orderByMore = false) {
		var defaultInfosFields = {
<#infos#>
        };
		return this.mergeInfoFields(defaultInfosFields, moreInfosFields, orderByMore);
    }

}
