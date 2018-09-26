import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { TenantPrintComponent } from './tenant-print.component';
import { TenantPrintRoutingModule } from './tenant-print.routing.module';

import { TenantService } from '../tenant.service';
import { ApiService } from '../../../common/services/api.service';
import { TenantServiceFields } from '../tenant.service.fields';

import { TenantContainerDetailsComponent } from '../tenant-container-details/tenant-container-details.component';
import { TenantFieldDetailsComponent } from '../tenant-field-details/tenant-field-details.component';
import { CommonSharedModule } from '../../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        CommonSharedModule,
        TenantPrintRoutingModule,
        FormsModule
    ],
    declarations: [
        TenantPrintComponent,
        TenantContainerDetailsComponent,
        TenantFieldDetailsComponent
    ],
    providers: [TenantService, ApiService, TenantServiceFields],
    exports: [TenantContainerDetailsComponent,TenantFieldDetailsComponent]
})
export class TenantPrintModule {

}
