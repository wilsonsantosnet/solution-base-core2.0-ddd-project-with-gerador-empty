import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';

import { ModalModule } from 'ngx-bootstrap/modal';

import { <#className#>Component } from './<#classNameLowerAndSeparator#>.component';

import { <#className#>ContainerFilterComponent } from './<#classNameLowerAndSeparator#>-container-filter/<#classNameLowerAndSeparator#>-container-filter.component';
import { <#className#>FieldFilterComponent } from './<#classNameLowerAndSeparator#>-field-filter/<#classNameLowerAndSeparator#>-field-filter.component';

import { <#className#>EditComponent } from './<#classNameLowerAndSeparator#>-edit/<#classNameLowerAndSeparator#>-edit.component';
import { <#className#>CreateComponent } from './<#classNameLowerAndSeparator#>-create/<#classNameLowerAndSeparator#>-create.component';
import { <#className#>DetailsComponent } from './<#classNameLowerAndSeparator#>-details/<#classNameLowerAndSeparator#>-details.component';

import { <#className#>FieldCreateComponent } from './<#classNameLowerAndSeparator#>-field-create/<#classNameLowerAndSeparator#>-field-create.component';
import { <#className#>FieldEditComponent } from './<#classNameLowerAndSeparator#>-field-edit/<#classNameLowerAndSeparator#>-field-edit.component';

import { <#className#>ContainerCreateComponent } from './<#classNameLowerAndSeparator#>-container-create/<#classNameLowerAndSeparator#>-container-create.component';
import { <#className#>ContainerEditComponent } from './<#classNameLowerAndSeparator#>-container-edit/<#classNameLowerAndSeparator#>-container-edit.component';

import { <#className#>ContainerDetailsComponent } from './<#classNameLowerAndSeparator#>-container-details/<#classNameLowerAndSeparator#>-container-details.component';
import { <#className#>FieldDetailsComponent } from './<#classNameLowerAndSeparator#>-field-details/<#classNameLowerAndSeparator#>-field-details.component';

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
        <#className#>ContainerFilterComponent,
        <#className#>FieldFilterComponent,
        <#className#>EditComponent,
        <#className#>CreateComponent,
        <#className#>DetailsComponent,
        <#className#>FieldCreateComponent,
        <#className#>FieldEditComponent,
        <#className#>ContainerCreateComponent,
        <#className#>ContainerEditComponent,
        <#className#>ContainerDetailsComponent,
        <#className#>FieldDetailsComponent

    ],
    providers: [<#className#>Service,<#className#>ServiceFields, ApiService],
	exports: [<#className#>Component, <#className#>EditComponent, <#className#>CreateComponent, <#className#>ContainerDetailsComponent, <#className#>FieldDetailsComponent]
})
export class <#className#>Module {


}
