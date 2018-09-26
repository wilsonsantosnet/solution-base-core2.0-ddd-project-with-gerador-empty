import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { ServiceBase } from '../../common/services/service.base';

@Injectable()
export class ProgramServiceFields extends ServiceBase {


    constructor() {
		super()
	}

	getKey() {
		return "Program";
	}

	getFormControls(moreFormControls? : any) {
		var formControls = Object.assign({
            description : new FormControl(),
            datasource : new FormControl(),
            databaseName : new FormControl(),
            programId : new FormControl(),
            themaId : new FormControl(),
        },moreFormControls || {});
		return formControls;
	}

	getFormFields(moreFormControls?: any) {
		return new FormGroup(this.getFormControls(moreFormControls));
	}

	getInfosFields(moreInfosFields? : any, orderByMore = false) {
		var defaultInfosFields = {
                    description: { label: 'description', type: 'string', isKey: false, list:true   },
                    datasource: { label: 'datasource', type: 'string', isKey: false, list:true   },
                    databaseName: { label: 'databaseName', type: 'string', isKey: false, list:true   },
                    programId: { label: 'programId', type: 'int', isKey: true, list:false   },
                    themaId: { label: 'themaId', type: 'int', isKey: false, list:true   },
        };
		return this.mergeInfoFields(defaultInfosFields, moreInfosFields, orderByMore);
    }

}
