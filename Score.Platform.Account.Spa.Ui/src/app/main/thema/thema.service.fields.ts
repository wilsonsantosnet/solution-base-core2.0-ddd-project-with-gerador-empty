import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { ServiceBase } from '../../common/services/service.base';

@Injectable()
export class ThemaServiceFields extends ServiceBase {


    constructor() {
		super()
	}

	getKey() {
		return "Thema";
	}

	getFormControls(moreFormControls? : any) {
		var formControls = Object.assign({
            name : new FormControl(),
            description : new FormControl(),
            themaId : new FormControl(),
        },moreFormControls || {});
		return formControls;
	}

	getFormFields(moreFormControls?: any) {
		return new FormGroup(this.getFormControls(moreFormControls));
	}

	getInfosFields(moreInfosFields? : any, orderByMore = false) {
		var defaultInfosFields = {
                    name: { label: 'name', type: 'string', isKey: false, list:true   },
                    description: { label: 'description', type: 'string', isKey: false, list:true   },
                    themaId: { label: 'themaId', type: 'int', isKey: true, list:false   },
        };
		return this.mergeInfoFields(defaultInfosFields, moreInfosFields, orderByMore);
    }

}
