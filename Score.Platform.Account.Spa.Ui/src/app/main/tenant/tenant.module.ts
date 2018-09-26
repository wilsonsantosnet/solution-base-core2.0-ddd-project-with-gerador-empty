import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { TenantComponent } from './tenant.component';

import { TenantContainerFilterComponent } from './tenant-container-filter/tenant-container-filter.component';
import { TenantFieldFilterComponent } from './tenant-field-filter/tenant-field-filter.component';

import { TenantEditComponent } from './tenant-edit/tenant-edit.component';
import { TenantCreateComponent } from './tenant-create/tenant-create.component';
import { TenantDetailsComponent } from './tenant-details/tenant-details.component';

import { TenantFieldCreateComponent } from './tenant-field-create/tenant-field-create.component';
import { TenantFieldEditComponent } from './tenant-field-edit/tenant-field-edit.component';

import { TenantContainerCreateComponent } from './tenant-container-create/tenant-container-create.component';
import { TenantContainerEditComponent } from './tenant-container-edit/tenant-container-edit.component';

import { TenantPrintModule } from './tenant-print/tenant-print.module';
import { TenantRoutingModule } from './tenant.routing.module';

import { TenantService } from './tenant.service';
import { TenantServiceFields } from './tenant.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';


@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        TenantRoutingModule,
        TenantPrintModule,

    ],
    declarations: [
        TenantComponent,
        TenantContainerFilterComponent,
        TenantFieldFilterComponent,
        TenantEditComponent,
        TenantCreateComponent,
        TenantDetailsComponent,
        TenantFieldCreateComponent,
        TenantFieldEditComponent,
        TenantContainerCreateComponent,
        TenantContainerEditComponent
    ],
    providers: [TenantService,TenantServiceFields, ApiService],
	exports: [TenantComponent, TenantEditComponent, TenantCreateComponent]
})
export class TenantModule {


}
