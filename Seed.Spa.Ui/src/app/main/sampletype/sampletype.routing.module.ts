import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { SampleTypeComponent } from './sampletype.component';
import { SampleTypeEditComponent } from './sampletype-edit/sampletype-edit.component';
import { SampleTypeDetailsComponent } from './sampletype-details/sampletype-details.component';
import { SampleTypeCreateComponent } from './sampletype-create/sampletype-create.component';
import { GlobalService } from '../../global.service';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "SampleType" }, component: SampleTypeComponent },
            { path: 'edit/:id', data : { title : "SampleType" } ,component: SampleTypeEditComponent },
            { path: 'details/:id', data : { title : "SampleType" }, component: SampleTypeDetailsComponent },
            { path: 'create', data : { title : "SampleType" }, component: SampleTypeCreateComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class SampleTypeRoutingModule {
}