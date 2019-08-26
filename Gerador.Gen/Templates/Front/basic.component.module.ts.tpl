import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';
import { <#className#>Component } from './<#classNameLowerAndSeparator#>.component';
import { <#className#>RoutingModule } from './<#classNameLowerAndSeparator#>.routing.module';

import { <#className#>Service } from './<#classNameLowerAndSeparator#>.service';
import { <#className#>ServiceFields } from './<#classNameLowerAndSeparator#>.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        <#className#>RoutingModule,
    ],
    declarations: [
        <#className#>Component
    ],
    providers: [<#className#>Service,<#className#>ServiceFields, ApiService],
})
export class <#className#>Module {


}