import { Component, NgModule, OnInit, OnDestroy, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { ApiService } from "../../common/services/api.service";
import { GlobalService, NotificationParameters } from "../../global.service";
import { ViewModel } from '../model/viewmodel';
@Component({
  selector: 'multiselect-funnel',
  template: `
      <div class="row" class="custom-funnel-row">
        <section class="col-md-5 custom-funnel-column">
            <input type="text" [(ngModel)]='_filterFunnel' class="form-control" name="filter_funnel" (keyup)="onFilterFunnel($event)">
            <div class='checkbox'>
                <label>
                    <input [(ngModel)]='_datasourceAll' type='checkbox' (change)='onSelectAllDataSource($event)' /> Todos
                </label>
            </div>
            <hr>
            <label>Disponiveis:</label>
            <div class='checkbox' *ngFor="let option of _datasource">
                <label>
                    <input type='checkbox' [(ngModel)]='option.checked' name='{{ctrlNameItem}}' value='{{option.id}}' (change)='onChange($event)'
                    /> {{ option.name }}
                </label>
            </div>
        </section>
        <section class="col-md-2 custom-funnel-btn">
            <button class="btn btn-default" type="button" (click)="onTransferenciaToLeft()">
                >> </button>
            <button class="btn btn-default" type="button" (click)="onTransferenciaToRigth()">
                << </button>
        </section>
        <section class="col-md-5 custom-funnel-column">
            <div class='checkbox'>
                <label>
                    <input [(ngModel)]='_datasourceFunnelAll' type='checkbox' (change)='onSelectAllDataSourceFunnel($event)' /> Todos
                </label>
            </div>
            <hr>
            <label>Selecionados:</label>
            <div class='checkbox' *ngFor="let option_funnel of _datasource_funnel">
                <label>
                    <input type='checkbox' [(ngModel)]='option_funnel.checked' name='{{ctrlNameItem}}' value='{{option_funnel.id}}' /> {{ option_funnel.name }}
                </label>
            </div>
        </section>
      </div>
    `
})
export class MultiSelectFunnelComponent implements OnInit, OnDestroy {
  @Input() dataitem: string;
  @Input() datafilters: any;
  @Input() vm: ViewModel<any>
  @Input() endpoint: string;
  @Input() ctrlName: string;
  @Input() ctrlNameItem: string;
  @Input() disabledOnInit: boolean;
  @Input() fieldFilterName: any;
  _datasource: any[];
  _datasourceAll: boolean;
  _datasourceFunnelAll: boolean;
  _datasource_funnel: any[];
  _selectedTemp: any[];
  _modelOutput: any[];
  _collectionjsonTemplate: any;
  _modelInput: any;
  _filter: any;
  _filterFunnel: string
  _notificationEmitter: EventEmitter<NotificationParameters>;
  _filteronstop: any;
  constructor(private api: ApiService<any>) {
    this._filter = {};
    this.fieldFilterName = "nome";
    this._notificationEmitter = new EventEmitter<NotificationParameters>();
    this._filteronstop = null;
  }
  ngOnInit() {
    this.init();
    var filterforEdit = this.createFiltersForEdit();
    if (!this.disabledOnInit) {
      this.getInstance();
    } else {
      if (filterforEdit)
        this.getInstance(filterforEdit);
    }
    this._notificationEmitter = GlobalService.getNotificationEmitter().subscribe((not: any) => {
      if (not.event == "edit" || not.event == "create" || not.event == "init") {
        this.init();
      }
      if (not.event == "change") {
        if (not.data.dataitem == this.dataitem)
          this.getInstance(not.data.parentFilter);
      }
    })
  }
  init() {
    this._selectedTemp = [];
    this._modelOutput = [];
    this._datasource = [];
    this._datasource_funnel = [];
    this._modelInput = this.vm.model[this.ctrlName];
    this._collectionjsonTemplate = "";
  }
  onSelectAllDataSource(e: any) {
    for (var i in this._datasource) {
      this._datasource[i].checked = this._datasourceAll;
    }
  }
  onSelectAllDataSourceFunnel(e: any) {
    for (var i in this._datasource_funnel) {
      this._datasource_funnel[i].checked = this._datasourceFunnelAll;
    }
  }
  onFilterFunnel(e: any) {
    if (this._filteronstop)
      clearTimeout(this._filteronstop)
    this._filteronstop = setTimeout(() => {
      this._datasource = [];
      var filterFunnel: any = {};
      filterFunnel[this.fieldFilterName] = this._filterFunnel;
      this.getInstance(filterFunnel)
    }, 500)
  }
  onChange(e: any) {
  }
  onTransferenciaToLeft() {
    var removeables = [];
    for (var i in this._datasource) {
      if (this._datasource[i].checked) {
        this._datasource[i].checked = false;
        this._datasource_funnel.push(this._datasource[i]);
        removeables.push(this._datasource[i].id);
      }
    }
    removeables.forEach(itemRemoveable => {
      this._datasource = this._datasource.filter((item: any) => {
        return item.id != itemRemoveable;
      });
    });
    this.updateModelOutputFunnel();
    this._selectedTemp = [];
  }
  onTransferenciaToRigth() {
    var removeables = [];
    for (let i in this._datasource_funnel) {
      if (this._datasource_funnel[i].checked) {
        this._datasource_funnel[i].checked = false;
        this._datasource.push(this._datasource_funnel[i]);
        removeables.push(this._datasource_funnel[i].id);
      }
    }
    removeables.forEach(itemRemoveable => {
      this._datasource_funnel = this._datasource_funnel.filter((item: any) => {
        return item.id != itemRemoveable;
      });
    });
    this.updateModelOutputFunnel();
    this._selectedTemp = [];
  }
  updateModelOutputFunnel() {
    this._modelOutput = [];
    this._datasource_funnel.forEach((item) => {
      this._modelOutput.push(item.id);
    })
    this.serializer();
  }
  private serializer() {
    this.vm.model[this.ctrlName] = this.serializeToSave();
  }
  private serializeToSave() {
    let items: any = [];
    for (let item in this._modelOutput) {
      items.push(`{ "${this.ctrlNameItem}" : "${this._modelOutput[item]}"}`);
    }
    this._collectionjsonTemplate = `[ ${items.join()} ]`;
    return JSON.parse(this._collectionjsonTemplate);
  }
  createFiltersForEdit() {
    if (this._modelInput) {
      let filters: any = {};
      filters.ids = this._modelInput.map((item: any) => {
        return item[this.ctrlNameItem];
      });
      return filters.ids.length > 0 ? filters : null;
    }
  }
  private getInstance(parentFilter?: any) {
    let filters = Object.assign(this.datafilters || {}, parentFilter || {});
    this.getInstanceMultiSelect(filters);
  }
  private getInstanceMultiSelect(filters: any) {
    this.api.setResource(this.dataitem, this.endpoint).getDataitem(filters).subscribe(result => {
      this._datasource = [];
      for (let item in result.dataList) {
        this._datasource.push({
          id: result.dataList[item].id,
          name: result.dataList[item].name,
          checked: this._modelInput ? this._modelInput.filter((selecteds: any) => {
            return selecteds[this.ctrlNameItem] == result.dataList[item].id;
          }).length > 0 : false
        });
      }
      this.onTransferenciaToLeft();
    });
  }
  ngOnDestroy() {
    if (this._notificationEmitter)
      this._notificationEmitter.unsubscribe();
  }
}
