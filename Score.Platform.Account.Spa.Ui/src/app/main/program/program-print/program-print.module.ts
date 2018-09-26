import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ProgramPrintComponent } from './program-print.component';
import { ProgramPrintRoutingModule } from './program-print.routing.module';

import { ProgramService } from '../program.service';
import { ApiService } from '../../../common/services/api.service';
import { ProgramServiceFields } from '../program.service.fields';

import { ProgramContainerDetailsComponent } from '../program-container-details/program-container-details.component';
import { ProgramFieldDetailsComponent } from '../program-field-details/program-field-details.component';
import { CommonSharedModule } from '../../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        CommonSharedModule,
        ProgramPrintRoutingModule,
        FormsModule
    ],
    declarations: [
        ProgramPrintComponent,
        ProgramContainerDetailsComponent,
        ProgramFieldDetailsComponent
    ],
    providers: [ProgramService, ApiService, ProgramServiceFields],
    exports: [ProgramContainerDetailsComponent,ProgramFieldDetailsComponent]
})
export class ProgramPrintModule {

}
