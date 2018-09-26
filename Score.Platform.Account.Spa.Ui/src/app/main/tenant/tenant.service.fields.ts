import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { ServiceBase } from '../../common/services/service.base';

@Injectable()
export class TenantServiceFields extends ServiceBase {


    constructor() {
		super()
	}

	getKey() {
		return "Tenant";
	}

	getFormControls(moreFormControls? : any) {
		var formControls = Object.assign({
            name : new FormControl(),
            email : new FormControl(),
            password : new FormControl(),
            guidResetPassword : new FormControl(),
            dateResetPassword : new FormControl(),
            tenantId : new FormControl(),
            programId : new FormControl(),
            active : new FormControl(),
            changePasswordNextLogin : new FormControl(),
        },moreFormControls || {});
		return formControls;
	}

	getFormFields(moreFormControls?: any) {
		return new FormGroup(this.getFormControls(moreFormControls));
	}

	getInfosFields(moreInfosFields? : any, orderByMore = false) {
		var defaultInfosFields = {
                    name: { label: 'name', type: 'string', isKey: false, list:true   },
                    email: { label: 'email', type: 'string', isKey: false, list:true   },
                    password: { label: 'password', type: 'string', isKey: false, list:true   },
                    guidResetPassword: { label: 'guidResetPassword', type: 'Guid?', isKey: false, list:true   },
                    dateResetPassword: { label: 'dateResetPassword', type: 'DateTime?', isKey: false, list:true   },
                    tenantId: { label: 'tenantId', type: 'int', isKey: true, list:false   },
                    programId: { label: 'programId', type: 'int', isKey: false, list:true   },
                    active: { label: 'active', type: 'bool', isKey: false, list:true   },
                    changePasswordNextLogin: { label: 'changePasswordNextLogin', type: 'bool', isKey: false, list:true   },
        };
		return this.mergeInfoFields(defaultInfosFields, moreInfosFields, orderByMore);
    }

}
