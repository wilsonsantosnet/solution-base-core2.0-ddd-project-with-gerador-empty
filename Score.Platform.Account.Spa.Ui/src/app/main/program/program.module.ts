import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { ProgramComponent } from './program.component';

import { ProgramContainerFilterComponent } from './program-container-filter/program-container-filter.component';
import { ProgramFieldFilterComponent } from './program-field-filter/program-field-filter.component';

import { ProgramEditComponent } from './program-edit/program-edit.component';
import { ProgramCreateComponent } from './program-create/program-create.component';
import { ProgramDetailsComponent } from './program-details/program-details.component';

import { ProgramFieldCreateComponent } from './program-field-create/program-field-create.component';
import { ProgramFieldEditComponent } from './program-field-edit/program-field-edit.component';

import { ProgramContainerCreateComponent } from './program-container-create/program-container-create.component';
import { ProgramContainerEditComponent } from './program-container-edit/program-container-edit.component';

import { ProgramPrintModule } from './program-print/program-print.module';
import { ProgramRoutingModule } from './program.routing.module';

import { ProgramService } from './program.service';
import { ProgramServiceFields } from './program.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        ProgramRoutingModule,
        ProgramPrintModule,

    ],
    declarations: [
        ProgramComponent,
        ProgramContainerFilterComponent,
        ProgramFieldFilterComponent,
        ProgramEditComponent,
        ProgramCreateComponent,
        ProgramDetailsComponent,
        ProgramFieldCreateComponent,
        ProgramFieldEditComponent,
        ProgramContainerCreateComponent,
        ProgramContainerEditComponent
    ],
    providers: [ProgramService,ProgramServiceFields, ApiService],
	exports: [ProgramComponent, ProgramEditComponent, ProgramCreateComponent]
})
export class ProgramModule {


}
