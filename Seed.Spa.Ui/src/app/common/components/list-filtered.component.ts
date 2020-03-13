import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';

import { GlobalService } from '../../global.service';
import { ViewModel } from 'src/app/common/model/viewmodel';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/common/services/api.service';

@Component({
  selector: 'list-filtered',
  template: `   <section class="col-md-12">
                    <div class="form-group">
                        <label class="mr-1">{{ vm.infos | traduction:selectFirstLabelTitle }}</label>
                        <select datasource class="form-control" 
                            name="{{selectFirstFieldKey}}" 
                            [(ngModel)]="formComponent[selectFirstFieldKey]" 
                            [dataitem]="selectFirstDataItem" 
                            [fieldFilterName]="selectFirstFieldDescription" 
                            (change)="filterChild()" 
                            [datafilters]="parentDataFilters">
                        </select>
                    </div>
                </section>

                <section class="col-md-12" *ngIf="showChild && formComponent[selectFirstFieldKey] > 0">
                    <div class="form-group">
                    <label class="mr-1">{{ vm.infos | traduction:selectSecondLabelTitle }}</label>
                    <div class="form-row">
                        <div class="col">
                                <select datasource class="form-control"
                                    name="{{selectSecondFieldKey}}"
                                    [fieldFilterName]="selectSecondFieldDescription" 
                                    [(ngModel)]="formComponent[selectSecondFieldKey]"
                                    [dataitem]="selectSecondDataItem" 
                                    [datafilters]="childDataFilters">
                                </select>
                        </div>
                        <div class="col-auto">
                            <button type="button" class="btn btn-sm btn-primary" (click)="addItem()"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>
                    </div>
                </section>

                <section class="col-md-12">
                <table class="table table-bordered table-hover table-stripped">
                    <colgroup>
                        <col>
                        <col>
                        <col width="1%">
                    </colgroup>
                    <thead class="thead-light">
                        <tr>
                            <th>{{selectFirstLabelTitle}}</th>
                            <th>{{selectSecondLabelTitle}}</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr *ngFor="let item of this.vm.model[this.ctrlName]">
                            <th>{{item | navigationProperty: selectFirstFieldDescription}}</th>
                            <th>{{item | navigationProperty: selectSecondFieldDescription}}</th>
                            <td><span class="text-danger cursor" (click)="removeItem(item.description)" title="{{ vm.infos | traduction:'Excluir' }}"><i class="fa fa-trash"></i></span></td>
                        </tr>
                    </tbody>
                </table>
            </section>`
})
export class ListFilteredComponent implements OnInit {

  @Input() vm: ViewModel<any>;
  @Input() datafilters: any;
  @Input() parentDataFilters: any;
  @Input() childDataFilters: any;
  @Input() confirmDataFilters: any;

  @Input() ctrlName: string;
  @Input() ctrlFieldKey: string;

  @Input() selectFirstLabelTitle: string;
  @Input() selectFirstDataItem: string;
  @Input() selectFirstFieldKey: string;
  @Input() selectFirstFieldDescription: string;

  @Input() selectSecondLabelTitle: string;
  @Input() selectSecondDataItem: string;
  @Input() selectSecondFieldKey: string;
  @Input() selectSecondFieldDescription: string;

  public formComponent: any;
  public showChild: boolean;

  constructor(private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef, private api: ApiService<any>) {

  }

  ngOnInit() {
    this.formComponent = {};
    this.parentDataFilters = Object.assign(this.parentDataFilters || Object.assign(this.datafilters || {}));
    this.childDataFilters = Object.assign(this.childDataFilters || Object.assign(this.datafilters || {}));
    this.confirmDataFilters = Object.assign(this.confirmDataFilters || Object.assign(this.datafilters || {}));
  }

  ngOnDestroy() {

  }

  filterChild() {
    this.showChild = false;

    let filter = Object.assign(this.datafilters || {});
    filter[this.selectFirstFieldKey] = this.formComponent[this.selectFirstFieldKey];
    this.api.setResource(this.selectFirstDataItem).getDataitem(filter).subscribe((data) => {
      this.setValue(this.formComponent, this.selectFirstFieldDescription, data.dataList[0].name);
      this.childDataFilters[this.selectFirstFieldKey] = this.formComponent[this.selectFirstFieldKey];

      setTimeout(() => {
        this.showChild = true;
      }, 250);
    });
  }

  addItem() {
    if (this.isValid()) {
      let filter = Object.assign(this.confirmDataFilters || {});
      filter[this.selectSecondFieldKey] = this.formComponent[this.selectSecondFieldKey];

      this.api.setResource(this.selectSecondDataItem).getDataitem(filter).subscribe((data) => {
        let itemToAdd = {};
        this.setValue(itemToAdd, this.ctrlFieldKey, data.dataList[0].id);
        this.setValue(itemToAdd, this.selectSecondFieldDescription, data.dataList[0].name);
        this.vm.model[this.ctrlName].push(Object.assign({}, itemToAdd));
      });
    }
  }

  removeItem(description) {
    this.vm.model[this.ctrlName] = this.vm.model[this.ctrlName].filter((obj) => {
      return obj.description !== description;
    });
  }

  setValue(component, field, valor) {
    if (field.includes('.')) {
      let node: any = {};
      for (let i = 0; i < field.split('.').length; i++) {

        node = field.split('.')[i];

        if (component[node] == undefined) {
          component[node] = {};
        }

        if (i < field.split('.').length - 1) {
          component = component[node];
        } else {
          component[node] = valor;
        }
      }
    } else {
      component[field] = valor;
    }
  }

  isValid() {
    if (this.formComponent[this.selectSecondFieldKey] == undefined || this.formComponent[this.selectSecondFieldKey] == '') {
      GlobalService.messageShow("Selecione um item no campo " + this.selectSecondFieldDescription + ' para adicionar o registro');
      return false;
    }

    let item = this.vm.model[this.ctrlName].filter((obj) => {
      return obj[this.ctrlFieldKey] == this.formComponent[this.selectSecondFieldKey];
    });

    if (item[0]) {
      GlobalService.messageShow("Item já foi adicionado na lista");
      return false;
    }

    return true;
  }
}
