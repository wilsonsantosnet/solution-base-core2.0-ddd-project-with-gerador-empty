import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SamplePrintComponent } from './sample-print.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: SamplePrintComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class  SamplePrintRoutingModule {

}