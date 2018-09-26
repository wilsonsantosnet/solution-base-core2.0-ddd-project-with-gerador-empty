import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { ThemaPrintComponent } from './thema-print.component';
import { ThemaPrintRoutingModule } from './thema-print.routing.module';

import { ThemaService } from '../thema.service';
import { ApiService } from '../../../common/services/api.service';
import { ThemaServiceFields } from '../thema.service.fields';

import { ThemaContainerDetailsComponent } from '../thema-container-details/thema-container-details.component';
import { ThemaFieldDetailsComponent } from '../thema-field-details/thema-field-details.component';
import { CommonSharedModule } from '../../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        CommonSharedModule,
        ThemaPrintRoutingModule,
        FormsModule
    ],
    declarations: [
        ThemaPrintComponent,
        ThemaContainerDetailsComponent,
        ThemaFieldDetailsComponent
    ],
    providers: [ThemaService, ApiService, ThemaServiceFields],
    exports: [ThemaContainerDetailsComponent,ThemaFieldDetailsComponent]
})
export class ThemaPrintModule {

}
