import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { SamplePrintComponent } from './sample-print.component';
import { SamplePrintRoutingModule } from './sample-print.routing.module';

import { SampleService } from '../sample.service';
import { ApiService } from '../../../common/services/api.service';
import { SampleServiceFields } from '../sample.service.fields';

import { SampleContainerDetailsComponent } from '../sample-container-details/sample-container-details.component';
import { SampleFieldDetailsComponent } from '../sample-field-details/sample-field-details.component';
import { CommonSharedModule } from '../../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        CommonSharedModule,
        SamplePrintRoutingModule,
        FormsModule
    ],
    declarations: [
        SamplePrintComponent,
        SampleContainerDetailsComponent,
        SampleFieldDetailsComponent
    ],
    providers: [SampleService, ApiService, SampleServiceFields],
    exports: [SampleContainerDetailsComponent,SampleFieldDetailsComponent]
})
export class SamplePrintModule {

}
