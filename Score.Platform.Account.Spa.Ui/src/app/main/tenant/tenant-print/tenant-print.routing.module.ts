import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { TenantPrintComponent } from './tenant-print.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: TenantPrintComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class  TenantPrintRoutingModule {

}