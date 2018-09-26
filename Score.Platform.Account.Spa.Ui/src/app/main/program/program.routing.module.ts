import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProgramComponent } from './program.component';
import { ProgramEditComponent } from './program-edit/program-edit.component';
import { ProgramDetailsComponent } from './program-details/program-details.component';
import { ProgramCreateComponent } from './program-create/program-create.component';
import { GlobalService } from '../../global.service';


@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', data : { title : "Program" }, component: ProgramComponent },
            { path: 'edit/:id', data : { title : "Program" } ,component: ProgramEditComponent },
            { path: 'details/:id', data : { title : "Program" }, component: ProgramDetailsComponent },
            { path: 'create', data : { title : "Program" }, component: ProgramCreateComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class ProgramRoutingModule {
}