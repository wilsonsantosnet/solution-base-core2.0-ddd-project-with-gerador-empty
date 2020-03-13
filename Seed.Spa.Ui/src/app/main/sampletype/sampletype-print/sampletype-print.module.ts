import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SampleTypePrintComponent } from './sampletype-print.component';
import { SampleTypePrintRoutingModule } from './sampletype-print.routing.module';

import { SampleTypeService } from '../sampletype.service';
import { ApiService } from '../../../common/services/api.service';
import { SampleTypeServiceFields } from '../sampletype.service.fields';

import { SampleTypeContainerDetailsComponent } from '../sampletype-container-details/sampletype-container-details.component';
import { SampleTypeFieldDetailsComponent } from '../sampletype-field-details/sampletype-field-details.component';
import { CommonSharedModule } from '../../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        CommonSharedModule,
        SampleTypePrintRoutingModule,
        FormsModule
    ],
    declarations: [
        SampleTypePrintComponent,
        SampleTypeContainerDetailsComponent,
        SampleTypeFieldDetailsComponent
    ],
    providers: [SampleTypeService, ApiService, SampleTypeServiceFields],
    exports: [SampleTypeContainerDetailsComponent,SampleTypeFieldDetailsComponent]
})
export class SampleTypePrintModule {

}
