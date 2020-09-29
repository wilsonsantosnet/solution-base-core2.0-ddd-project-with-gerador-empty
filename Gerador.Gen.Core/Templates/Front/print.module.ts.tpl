import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { <#className#>PrintComponent } from './<#classNameLowerAndSeparator#>-print.component';
import { <#className#>PrintRoutingModule } from './<#classNameLowerAndSeparator#>-print.routing.module';

import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';
import { ApiService } from '../../../common/services/api.service';
import { <#className#>ServiceFields } from '../<#classNameLowerAndSeparator#>.service.fields';

import { <#className#>ContainerDetailsComponent } from '../<#classNameLowerAndSeparator#>-container-details/<#classNameLowerAndSeparator#>-container-details.component';
import { <#className#>FieldDetailsComponent } from '../<#classNameLowerAndSeparator#>-field-details/<#classNameLowerAndSeparator#>-field-details.component';
import { CommonSharedModule } from '../../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        CommonSharedModule,
        <#className#>PrintRoutingModule,
        FormsModule
    ],
    declarations: [
        <#className#>PrintComponent,
        <#className#>ContainerDetailsComponent,
        <#className#>FieldDetailsComponent
    ],
    providers: [<#className#>Service, ApiService, <#className#>ServiceFields],
    exports: [<#className#>ContainerDetailsComponent,<#className#>FieldDetailsComponent]
})
export class <#className#>PrintModule {

}
