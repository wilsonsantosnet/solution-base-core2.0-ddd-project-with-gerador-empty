//EXT
import { Injectable } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { Subject } from 'rxjs';
import { FormGroup, FormControl } from '@angular/forms';

import { ApiService } from '../../common/services/api.service';
import { ServiceBase } from '../../common/services/service.base';
import { ViewModel } from '../../common/model/viewmodel';
import { GlobalService } from '../../global.service';
import { <#className#>ServiceFields } from './<#classNameLowerAndSeparator#>.service.fields';
import { GlobalServiceCulture, Translated, TranslatedField } from '../../global.service.culture';
import { MainService } from '../main.service';

@Injectable()
export class <#className#>Service extends ServiceBase {

    private _form : FormGroup;

    constructor(private api: ApiService<any>,private serviceFields: <#className#>ServiceFields, private globalServiceCulture: GlobalServiceCulture, private mainService: MainService) {

        super();
        this._form = this.serviceFields.getFormFields();

    }

    initVM(): ViewModel<any> {

        return new ViewModel({
            mostrarFiltros: false,
            actionTitle: " <#classNameFormated#>",
            actionDescription: "",
            downloadUri: GlobalService.getEndPoints().DOWNLOAD,
            filterResult: [],
            modelFilter: {},
            summary: {},
            model: {},
            details: {},
            infos: this.getInfos(),
            grid: this.getInfoGrid(this.getInfos()),
            generalInfo: this.mainService.getInfos(),
            form: this._form,
            masks: this.masksConfig()
        });
    }

    getInfos() {
        return this.serviceFields.getInfosFields();
    }

    getInfoGrid(infos : any) {
        return super.getInfoGrid(infos)
    }

    updateCulture(culture: string = null) {
        return this.getInfosTranslated(this.globalServiceCulture.defineCulture(culture));
    }

    updateCultureMain(culture: string = null) {
        return this.mainService.getInfosTranslated(this.globalServiceCulture.defineCulture(culture));
    }

    getInfosTranslated(culture: string) {
        return this.globalServiceCulture.getInfosTranslatedStrategy('<#className#>', culture, this.getInfos(), []);
    }
    get(resource : string ,filters?: any): Observable<any> {
        return this.api.setResource(resource).get(filters);
    }

    getDataCustom(resource: string,filters?: any): Observable<any> {
        return this.api.setResource(resource).getDataCustom(filters);
    }

    getDataListCustom(resource: string,filters?: any): Observable<any> {
        return this.api.setResource(resource).getDataListCustom(filters);
    }

    getDataListCustomPaging(resource: string,filters?: any): Observable<any> {
        return this.api.setResource(resource).getDataListCustomPaging(filters);
    }
    getDataitem(resource: string, filters?: any): Observable<any> {
        return this.api.setResource(resource).getDataitem(filters);
    }
    save(resource: string,model: any): Observable<any> {

        if (model.alunoId) {
          return this.api.setResource(resource).put(model);
        }

        return this.api.setResource(resource).post(model);
    }
    delete(resource: string,model: any): Observable<any> {
        return this.api.setResource(resource).delete(model);
    }
    export(resource: string,filters?: any): Observable<any> {
        return this.api.setResource(resource).export(filters);
    }
}
