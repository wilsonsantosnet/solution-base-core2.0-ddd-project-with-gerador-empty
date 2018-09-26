import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './common/services/auth.guard';

const APP_ROUTES_DEFAULT: Routes = [

    {
        path: '', component: MainComponent, data : { title : "Main" }, children: [

            { path: 'program',  canActivate: [AuthGuard], loadChildren: './main/program/program.module#ProgramModule' },

            { path: 'tenant',  canActivate: [AuthGuard], loadChildren: './main/tenant/tenant.module#TenantModule' },

            { path: 'thema',  canActivate: [AuthGuard], loadChildren: './main/thema/thema.module#ThemaModule' },



            ]
    },

    { path: 'program/print/:id', canActivate: [AuthGuard], loadChildren: './main/program/program-print/program-print.module#ProgramPrintModule' },

    { path: 'tenant/print/:id', canActivate: [AuthGuard], loadChildren: './main/tenant/tenant-print/tenant-print.module#TenantPrintModule' },

    { path: 'thema/print/:id', canActivate: [AuthGuard], loadChildren: './main/thema/thema-print/thema-print.module#ThemaPrintModule' },


]


export const RoutingDefault: ModuleWithProviders = RouterModule.forRoot(APP_ROUTES_DEFAULT);


