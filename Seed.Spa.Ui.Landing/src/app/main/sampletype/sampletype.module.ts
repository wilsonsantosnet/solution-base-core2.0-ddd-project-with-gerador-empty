import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { SampleTypeComponent } from './sampletype.component';

import { SampleTypeContainerFilterComponent } from './sampletype-container-filter/sampletype-container-filter.component';
import { SampleTypeFieldFilterComponent } from './sampletype-field-filter/sampletype-field-filter.component';

import { SampleTypeEditComponent } from './sampletype-edit/sampletype-edit.component';
import { SampleTypeCreateComponent } from './sampletype-create/sampletype-create.component';
import { SampleTypeDetailsComponent } from './sampletype-details/sampletype-details.component';

import { SampleTypeFieldCreateComponent } from './sampletype-field-create/sampletype-field-create.component';
import { SampleTypeFieldEditComponent } from './sampletype-field-edit/sampletype-field-edit.component';

import { SampleTypeContainerCreateComponent } from './sampletype-container-create/sampletype-container-create.component';
import { SampleTypeContainerEditComponent } from './sampletype-container-edit/sampletype-container-edit.component';

import { SampleTypePrintModule } from './sampletype-print/sampletype-print.module';
import { SampleTypeRoutingModule } from './sampletype.routing.module';

import { SampleTypeService } from './sampletype.service';
import { SampleTypeServiceFields } from './sampletype.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        SampleTypeRoutingModule,
        SampleTypePrintModule,

    ],
    declarations: [
        SampleTypeComponent,
        SampleTypeContainerFilterComponent,
        SampleTypeFieldFilterComponent,
        SampleTypeEditComponent,
        SampleTypeCreateComponent,
        SampleTypeDetailsComponent,
        SampleTypeFieldCreateComponent,
        SampleTypeFieldEditComponent,
        SampleTypeContainerCreateComponent,
        SampleTypeContainerEditComponent
    ],
    providers: [SampleTypeService,SampleTypeServiceFields, ApiService],
	exports: [SampleTypeComponent, SampleTypeEditComponent, SampleTypeCreateComponent]
})
export class SampleTypeModule {


}
