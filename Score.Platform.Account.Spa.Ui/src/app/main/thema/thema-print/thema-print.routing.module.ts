import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { ThemaPrintComponent } from './thema-print.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: ThemaPrintComponent }
        ])
    ],
    exports: [
        RouterModule
    ]
})

export class  ThemaPrintRoutingModule {

}