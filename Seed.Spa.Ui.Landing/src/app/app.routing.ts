import { ModuleWithProviders } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { MainComponent } from './main/main.component';
import { LoginComponent } from './login/login.component';
import { AuthGuard } from './common/services/auth.guard';

const APP_ROUTES_DEFAULT: Routes = [

    {
        path: '', component: MainComponent, data : { title : "Main" }, children: [

			{ path: 'sample',  canActivate: [AuthGuard], loadChildren: () => import('./main/sample/sample.module').then(m => m.SampleModule) },

			{ path: 'sampletype',  canActivate: [AuthGuard], loadChildren: () => import('./main/sampletype/sampletype.module').then(m => m.SampleTypeModule) },

            { path: 'sampledash',  canActivate: [AuthGuard], loadChildren: () => import('./main/sampledash/sampledash.module').then(m => m.SampleDashModule) }

            ]
    },

      { path: 'sample/print/:id', canActivate: [AuthGuard], loadChildren: () => import('./main/sample/sample-print/sample-print.module').then(m => m.SamplePrintModule) },
      { path: 'sampletype/print/:id', canActivate: [AuthGuard], loadChildren: () => import('./main/sampletype/sampletype-print/sampletype-print.module').then(m => m.SampleTypePrintModule) },

]


export const RoutingDefault: ModuleWithProviders = RouterModule.forRoot(APP_ROUTES_DEFAULT);


