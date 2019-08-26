import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { <#className#>Component } from './<#classNameLowerAndSeparator#>.component';
import { <#className#>EditComponent } from './<#classNameLowerAndSeparator#>-edit/<#classNameLowerAndSeparator#>-edit.component';
import { <#className#>DetailsComponent } from './<#classNameLowerAndSeparator#>-details/<#classNameLowerAndSeparator#>-details.component';
import { <#className#>CreateComponent } from './<#classNameLowerAndSeparator#>-create/<#classNameLowerAndSeparator#>-create.component';
import { GlobalService } from '../../global.service';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "<#className#>" }, component: <#className#>Component },
            { path: 'edit/:id', data : { title : "<#className#>" } ,component: <#className#>EditComponent },
            { path: 'details/:id', data : { title : "<#className#>" }, component: <#className#>DetailsComponent },
            { path: 'create', data : { title : "<#className#>" }, component: <#className#>CreateComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class <#className#>RoutingModule {
}