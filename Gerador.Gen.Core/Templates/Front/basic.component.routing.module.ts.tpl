import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { <#className#>Component } from './<#classNameLowerAndSeparator#>.component';
import { GlobalService } from '../../global.service';

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