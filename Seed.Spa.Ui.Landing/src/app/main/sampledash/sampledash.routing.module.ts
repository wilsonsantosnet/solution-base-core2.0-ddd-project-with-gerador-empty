import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SampleDashComponent } from './sampledash.component';
import { GlobalService } from '../../global.service';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "SampleDash" }, component: SampleDashComponent },
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class SampleDashRoutingModule {

}