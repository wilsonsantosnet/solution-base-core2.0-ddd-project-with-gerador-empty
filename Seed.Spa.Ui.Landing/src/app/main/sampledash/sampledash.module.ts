import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';
import { SampleDashComponent } from './sampledash.component';
import { SampleDashRoutingModule } from './sampledash.routing.module';

import { SampleDashService } from './sampledash.service';
import { SampleDashServiceFields } from './sampledash.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        SampleDashRoutingModule,
    ],
    declarations: [
        SampleDashComponent
    ],
    providers: [SampleDashService,SampleDashServiceFields, ApiService],
})
export class SampleDashModule {


}