import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { ServiceBase } from '../../common/services/service.base';

@Injectable()
export class <#className#>ServiceFields extends ServiceBase {


	constructor() {
		super()
	}

	getKey() {
		return "<#className#>";
	}

	getFormControls(moreFormControls? : any) {
		var formControls = Object.assign({
<#riquered#>
		},moreFormControls || {});
		return formControls;
	}

	getFormFields(moreFormControls?: any) {
		return new FormGroup(this.getFormControls(moreFormControls));
	}

	getInfosFields(moreInfosFields? : any, useMoreInfosFields = false) {
		var defaultInfosFields = {
<#infos#>
		};
		return this.mergeInfoFields(defaultInfosFields, moreInfosFields, useMoreInfosFields);
	}

}
