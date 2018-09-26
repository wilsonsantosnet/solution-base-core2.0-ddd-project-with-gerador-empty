import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';

import { PaginationModule } from 'ngx-bootstrap/pagination'
import { TabsModule } from 'ngx-bootstrap/tabs';
import { ModalModule } from 'ngx-bootstrap/modal';
import { TagInputModule } from 'ngx-chips';
import { TextMaskModule } from 'angular2-text-mask';

import { DataSourceDirective } from './directives/select-datasource.directive';
import { MaskInputDirective } from './directives/mask-input.directive';
import { MaskMoneyDirective } from './directives/mask-money.directive';
import { UnMaskDirective } from './directives/unmask.directive';
import { DateDirective } from './directives/date.directive';
import { DateTimeDirective } from './directives/date.time.directive';
import { BindCustomComponent } from './components/bind-custom.component';
import { MakeGridComponent } from './components/grid.component'
import { CepComponent } from '../common/components/cep.component';
import { TreeViewComponent } from '../common/components/tree-view.component';
import { NestabaleTreeComponent } from '../common/components/nestable-tree.component';
import { UploadCustomComponent } from '../common/components/upload-file.component';
import { MultiSelectComponent } from '../common/components/multiselect.component';
import { DateFormatPipe } from './pipes/date-format.pipe';
import { MaskFormatPipe } from './pipes/mask.pipe';
import { LoopToPipe } from './pipes/loop-to.pipe';
import { ExistsRequestPipe } from './pipes/exists-request.pipe';
import { ContainsPipe } from './pipes/contains.pipe';
import { TraductionPipe } from './pipes/traduction.pipe';

import { EditorHtmlDiretive } from './directives/editor-html.directive';
import { TagCustomComponent } from '../common/components/tag.component';
import { DomElemetAppendDirective } from '../common/directives/dom-elemet-apped.directive';
import { NestableDirective } from '../common/directives/nestable.directive';
import { MultiSelectFunnelComponent } from '../common/components/multiselect-funnel.component';
import { CepDirective } from "../common/directives/cep.directive";
import { LoadingOnSideComponent } from '../common/components/loading-on-side.component';
import { MakePaginationComponent } from './components/pagination.component';
import { CallerDiretive } from '../common/directives/caller.directive';
import { DataSourceShowDirective } from '../common/directives/show-datasource.directive';
import { isAuthPipe } from './pipes/is-auth.pipe';
import { CpfCnpjComponent } from './components/cpf-cnpj.component';

@NgModule({
  imports: [
    CommonModule,
    ReactiveFormsModule,
    PaginationModule.forRoot(),
    ModalModule.forRoot(),
    FormsModule,
    TextMaskModule,
    TagInputModule,
    TabsModule.forRoot(),
  ],
  declarations: [
    BindCustomComponent,
    CpfCnpjComponent,
    MakePaginationComponent,
    DataSourceDirective,
    MaskInputDirective,
    MaskMoneyDirective,
    UnMaskDirective,
    DateDirective,
    DateTimeDirective,
    CepDirective,
    EditorHtmlDiretive,
    DomElemetAppendDirective,
    MakeGridComponent,
    CepComponent,
    TreeViewComponent,
    NestabaleTreeComponent,
    UploadCustomComponent,
    MultiSelectComponent,
    MultiSelectFunnelComponent,
    DateFormatPipe,
    MaskFormatPipe,
    LoopToPipe,
    ExistsRequestPipe,
    ContainsPipe,
    TraductionPipe,
    isAuthPipe,
    TagCustomComponent,
    CallerDiretive,
    DataSourceShowDirective,
    NestableDirective,
    LoadingOnSideComponent
  ],
  providers: [
  ],
  exports: [
    BindCustomComponent,
    MakePaginationComponent,
    CpfCnpjComponent,
    MakeGridComponent,
    CepComponent,
    CepDirective,
    TreeViewComponent,
    NestabaleTreeComponent,
    UploadCustomComponent,
    MultiSelectComponent,
    MultiSelectFunnelComponent,
    TagCustomComponent,
    DataSourceDirective,
    MaskInputDirective,
    MaskMoneyDirective,
    UnMaskDirective,
    DateDirective,
    DateTimeDirective,
    EditorHtmlDiretive,
    DomElemetAppendDirective,
    CallerDiretive,
    DataSourceShowDirective,
    NestableDirective,
    TextMaskModule,
    TagInputModule,
    TabsModule,
    LoadingOnSideComponent,
    DateFormatPipe,
    MaskFormatPipe,
    LoopToPipe,
    ExistsRequestPipe,
    ContainsPipe,
    TraductionPipe,
    isAuthPipe
  ]
})
export class CommonSharedModule {

}
