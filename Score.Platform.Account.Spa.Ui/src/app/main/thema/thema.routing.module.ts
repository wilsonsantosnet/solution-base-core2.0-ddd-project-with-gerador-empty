import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ThemaComponent } from './thema.component';
import { ThemaEditComponent } from './thema-edit/thema-edit.component';
import { ThemaDetailsComponent } from './thema-details/thema-details.component';
import { ThemaCreateComponent } from './thema-create/thema-create.component';
import { GlobalService } from '../../global.service';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "Thema" }, component: ThemaComponent },
            { path: 'edit/:id', data : { title : "Thema" } ,component: ThemaEditComponent },
            { path: 'details/:id', data : { title : "Thema" }, component: ThemaDetailsComponent },
            { path: 'create', data : { title : "Thema" }, component: ThemaCreateComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class ThemaRoutingModule {
}