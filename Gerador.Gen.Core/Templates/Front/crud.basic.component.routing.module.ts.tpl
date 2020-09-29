import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { <#className#>Component } from './<#classNameLowerAndSeparator#>.component';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "<#className#>" }, component: <#className#>Component },
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class <#className#>RoutingModule {
}