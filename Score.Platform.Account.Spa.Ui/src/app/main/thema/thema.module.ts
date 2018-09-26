import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { ThemaComponent } from './thema.component';

import { ThemaContainerFilterComponent } from './thema-container-filter/thema-container-filter.component';
import { ThemaFieldFilterComponent } from './thema-field-filter/thema-field-filter.component';

import { ThemaEditComponent } from './thema-edit/thema-edit.component';
import { ThemaCreateComponent } from './thema-create/thema-create.component';
import { ThemaDetailsComponent } from './thema-details/thema-details.component';

import { ThemaFieldCreateComponent } from './thema-field-create/thema-field-create.component';
import { ThemaFieldEditComponent } from './thema-field-edit/thema-field-edit.component';

import { ThemaContainerCreateComponent } from './thema-container-create/thema-container-create.component';
import { ThemaContainerEditComponent } from './thema-container-edit/thema-container-edit.component';

import { ThemaPrintModule } from './thema-print/thema-print.module';
import { ThemaRoutingModule } from './thema.routing.module';

import { ThemaService } from './thema.service';
import { ThemaServiceFields } from './thema.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        ThemaRoutingModule,
        ThemaPrintModule,

    ],
    declarations: [
        ThemaComponent,
        ThemaContainerFilterComponent,
        ThemaFieldFilterComponent,
        ThemaEditComponent,
        ThemaCreateComponent,
        ThemaDetailsComponent,
        ThemaFieldCreateComponent,
        ThemaFieldEditComponent,
        ThemaContainerCreateComponent,
        ThemaContainerEditComponent
    ],
    providers: [ThemaService,ThemaServiceFields, ApiService],
	exports: [ThemaComponent, ThemaEditComponent, ThemaCreateComponent]
})
export class ThemaModule {


}
