import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { SampleComponent } from './sample.component';

import { SampleContainerFilterComponent } from './sample-container-filter/sample-container-filter.component';
import { SampleFieldFilterComponent } from './sample-field-filter/sample-field-filter.component';

import { SampleEditComponent } from './sample-edit/sample-edit.component';
import { SampleCreateComponent } from './sample-create/sample-create.component';
import { SampleDetailsComponent } from './sample-details/sample-details.component';

import { SampleFieldCreateComponent } from './sample-field-create/sample-field-create.component';
import { SampleFieldEditComponent } from './sample-field-edit/sample-field-edit.component';

import { SampleContainerCreateComponent } from './sample-container-create/sample-container-create.component';
import { SampleContainerEditComponent } from './sample-container-edit/sample-container-edit.component';

import { SamplePrintModule } from './sample-print/sample-print.module';
import { SampleRoutingModule } from './sample.routing.module';

import { SampleService } from './sample.service';
import { SampleServiceFields } from './sample.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        SampleRoutingModule,
        SamplePrintModule,

    ],
    declarations: [
        SampleComponent,
        SampleContainerFilterComponent,
        SampleFieldFilterComponent,
        SampleEditComponent,
        SampleCreateComponent,
        SampleDetailsComponent,
        SampleFieldCreateComponent,
        SampleFieldEditComponent,
        SampleContainerCreateComponent,
        SampleContainerEditComponent
    ],
    providers: [SampleService,SampleServiceFields, ApiService],
	exports: [SampleComponent, SampleEditComponent, SampleCreateComponent]
})
export class SampleModule {


}
