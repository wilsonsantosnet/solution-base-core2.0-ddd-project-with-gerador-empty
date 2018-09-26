import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ProgramPrintComponent } from './program-print.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: ProgramPrintComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class  ProgramPrintRoutingModule {

}