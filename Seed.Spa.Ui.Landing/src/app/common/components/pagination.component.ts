import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';

import { GlobalService } from '../../global.service';
import { ApiService } from '../services/api.service';
import { ViewModel } from '../model/viewmodel';

@Component({
  selector: 'make-pagination',
  template: `
      <div class="row align-items-center">
        <div class="col">
          <pagination class="mb-0" 
             (pageChanged)="onPageChanged($event)" 
              [itemsPerPage]="vm.summary.pageSize" 
              [totalItems]="vm.summary.total" 
              [maxSize]="5"
              previousText="Anterior"
              nextText="Próximo">
          </pagination>
        </div>
        <div class="col" *ngIf=!disableSummary>
          <div class="pull-right">
            <strong>Total de registros: </strong> <span class="label label-primary">
              <bind-custom [model]="vm.summary.total" [format]="'integer'" [tag]="'span'"></bind-custom>
            </span>
          </div>
        </div>
      </div>`
})
export class MakePaginationComponent {

  @Input() vm: ViewModel<any>
  @Input() disableSummary: boolean
  @Output() pageChanged = new EventEmitter<any>();

  initialPage: number;

  constructor() {

    this.initialPage = 1;
  }

  onPageChanged(e: any) {

    this.pageChanged.emit({
      PageIndex: e.page,
      PageSize: e.itemsPerPage,
    })
  }

}
