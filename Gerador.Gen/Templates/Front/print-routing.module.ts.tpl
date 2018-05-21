import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { <#className#>PrintComponent } from './<#classNameLowerAndSeparator#>-print.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: <#className#>PrintComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class  <#className#>PrintRoutingModule {

}