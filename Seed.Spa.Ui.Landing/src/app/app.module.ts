import { BrowserModule } from '@angular/platform-browser';
import { NgModule, APP_INITIALIZER, LOCALE_ID } from '@angular/core';

import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpModule } from '@angular/http';
import { FormsModule } from '@angular/forms';

import { AuthService } from './common/services/auth.service';
import { ApiService } from './common/services/api.service';
import { ServiceBase } from './common/services/service.base';

import { ConfirmModalComponent } from './common/components/confirm-modal.component';
import { LoadingComponent } from './common/components/loading.component';
import { LoadingTopComponent } from './common/components/loading-top.component';
import { MenuComponent } from './common/components/menu.component';

import { AuthGuard } from './common/services/auth.guard';
import { LoginComponent } from './login/login.component';
import { MainComponent } from './main/main.component';
import { MainService } from './main/main.service';
import { AppComponent } from './app.component';
import { AvatarComponent } from './common/components/avatar.component';
import { SidebarToggleDirective } from './common/directives/sidebar.directive';
import { AsidebarToggleDirective } from './common/directives/asidebar.directive';

import { GlobalServiceCulture } from './global.service.culture';
import { StartupService } from './startup.service';

import { PerfectScrollbarModule } from 'ngx-perfect-scrollbar';
import { PERFECT_SCROLLBAR_CONFIG } from 'ngx-perfect-scrollbar';
import { PerfectScrollbarConfigInterface } from 'ngx-perfect-scrollbar';

import { PopoverModule } from 'ngx-bootstrap/popover';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { SimpleNotificationsModule } from 'angular2-notifications';
import { EEnumService } from './util/enum/enum.service';

import { RoutingDefault } from './app.routing';
import { RoutingCustom } from './app.custom.routing';
import { registerLocaleData } from '@angular/common';
import ptBr from '@angular/common/locales/pt';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { MessageModalComponent } from './common/components/message-modal.component';
import { BannerComponent } from './common/components/landing/banner/banner.component';
import { HeaderComponent } from './common/components/landing/header/header.component';
import { WelcomeComponent } from './common/components/landing/welcome/welcome.component';
import { IntroComponent } from './common/components/landing/intro/intro.component';
import { ContactComponent } from './common/components/landing/contact/contact.component';
import { FooterComponent } from './common/components/landing/footer/footer.component';



registerLocaleData(ptBr)

export function startupServiceFactory(startupService: StartupService): Function {
  return () => startupService.load();
}

@NgModule({
  declarations: [
    AppComponent,
    AvatarComponent,
    MainComponent,
    LoginComponent,
    UnauthorizedComponent,
    LoadingComponent,
    ConfirmModalComponent,
    MessageModalComponent,
    MenuComponent,
    LoadingTopComponent,
    BannerComponent,
    HeaderComponent,
    WelcomeComponent,
    IntroComponent,
    ContactComponent,
    FooterComponent,
    SidebarToggleDirective,
    AsidebarToggleDirective    
  ],
  imports: [
    BrowserModule,
    FormsModule,
    BrowserAnimationsModule,
    HttpModule,
    RoutingDefault,
    RoutingCustom,
    PerfectScrollbarModule,
    ModalModule.forRoot(),
    PopoverModule.forRoot(),
    SimpleNotificationsModule.forRoot(),
    TabsModule.forRoot(),
  ],
  providers: [
    HttpModule,
    StartupService,
    {
      provide : LOCALE_ID,
      useValue: 'pt-PT'
    },
    {
      provide: APP_INITIALIZER,
      useFactory: startupServiceFactory,
      deps: [StartupService],
      multi: true
    },
    AuthService,
    ApiService,
    AuthGuard,
    MainService,
    ServiceBase,
    GlobalServiceCulture,
    EEnumService

  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
