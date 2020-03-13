import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SampleTypePrintComponent } from './sampletype-print.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: SampleTypePrintComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class  SampleTypePrintRoutingModule {

}