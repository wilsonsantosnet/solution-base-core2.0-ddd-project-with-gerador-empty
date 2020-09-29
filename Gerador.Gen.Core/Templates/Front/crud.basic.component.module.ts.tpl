import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { <#className#>Component } from './<#classNameLowerAndSeparator#>.component';

import { <#className#>FieldCreateComponent } from './<#classNameLowerAndSeparator#>-field-create/<#classNameLowerAndSeparator#>-field-create.component';
import { <#className#>FieldEditComponent } from './<#classNameLowerAndSeparator#>-field-edit/<#classNameLowerAndSeparator#>-field-edit.component';

import { <#className#>ContainerCreateComponent } from './<#classNameLowerAndSeparator#>-container-create/<#classNameLowerAndSeparator#>-container-create.component';
import { <#className#>ContainerEditComponent } from './<#classNameLowerAndSeparator#>-container-edit/<#classNameLowerAndSeparator#>-container-edit.component';

import { <#className#>RoutingModule } from './<#classNameLowerAndSeparator#>.routing.module';

import { <#className#>Service } from './<#classNameLowerAndSeparator#>.service';
import { <#className#>ServiceFields } from './<#classNameLowerAndSeparator#>.service.fields';

import { ApiService } from '../../common/services/api.service';
import { CommonSharedModule } from '../../common/common-shared.module';
<#groupComponentModulesImport#>

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
        ModalModule.forRoot(),
        CommonSharedModule,
        <#className#>RoutingModule,
<#groupComponentModules#>
    ],
    declarations: [
        <#className#>Component,
        <#className#>FieldCreateComponent,
        <#className#>FieldEditComponent,
        <#className#>ContainerCreateComponent,
        <#className#>ContainerEditComponent
    ],
    providers: [<#className#>Service,<#className#>ServiceFields, ApiService],
	exports: [<#className#>Component]
})
export class <#className#>Module {


}
