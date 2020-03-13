import { Injectable } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

import { ServiceBase } from '../../common/services/service.base';

@Injectable()
export class SampleServiceFields extends ServiceBase {


    constructor() {
		super()
	}

	getKey() {
		return "Sample";
	}

	getFormControls(moreFormControls? : any) {
		var formControls = Object.assign({
            name : new FormControl(),
            descricao : new FormControl(),
            tags : new FormControl(),
            datetime : new FormControl(),
            sampleId : new FormControl(),
            sampleTypeId : new FormControl(),
            age : new FormControl(),
            category : new FormControl(),
            ativo : new FormControl(),
        },moreFormControls || {});
		return formControls;
	}

	getFormFields(moreFormControls?: any) {
		return new FormGroup(this.getFormControls(moreFormControls));
	}

	getInfosFields(moreInfosFields? : any, orderByMore = false) {
		var defaultInfosFields = {
                    name: { label: 'name', type: 'string', isKey: false, list:true  ,  },

                    descricao: { label: 'descricao', type: 'string', isKey: false, list:false  ,  },

                    tags: { label: 'tags', type: 'string', isKey: false, list:false  ,  },

                    datetime: { label: 'datetime', type: 'DateTime?', isKey: false, list:true  ,  },

                    sampleId: { label: 'sampleId', type: 'int', isKey: true, list:false  ,  },

                    sampleTypeId: { label: 'sampleTypeId', type: 'int', isKey: false, list:true  , navigationProp:'SampleType' },

                    age: { label: 'age', type: 'int?', isKey: false, list:true  ,  },

                    category: { label: 'category', type: 'int?', isKey: false, list:true  ,  },

                    ativo: { label: 'ativo', type: 'bool?', isKey: false, list:true  ,  },

        };
		return this.mergeInfoFields(defaultInfosFields, moreInfosFields, orderByMore);
    }

}
