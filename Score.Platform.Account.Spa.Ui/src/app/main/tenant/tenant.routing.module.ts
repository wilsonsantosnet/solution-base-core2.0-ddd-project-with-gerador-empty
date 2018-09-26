import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TenantComponent } from './tenant.component';
import { TenantEditComponent } from './tenant-edit/tenant-edit.component';
import { TenantDetailsComponent } from './tenant-details/tenant-details.component';
import { TenantCreateComponent } from './tenant-create/tenant-create.component';
import { GlobalService } from '../../global.service';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "Tenant" }, component: TenantComponent },
            { path: 'edit/:id', data : { title : "Tenant" } ,component: TenantEditComponent },
            { path: 'details/:id', data : { title : "Tenant" }, component: TenantDetailsComponent },
            { path: 'create', data : { title : "Tenant" }, component: TenantCreateComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class TenantRoutingModule {
}