import { Component, NgModule, OnInit, OnDestroy, Input, Output, EventEmitter, ViewChild } from '@angular/core';

//import { ApiService } from "app/common/services/api.service";
import { GlobalService, NotificationParameters } from "../../global.service";
import { ViewModel } from '../model/viewmodel';
import { ApiService } from '../services/api.service';
import { Subscription } from 'rxjs';

declare var $: any;

@Component({
  selector: 'multiselect',
  template: `<section *ngIf="!enabledSelect2" class="col-md-12 section-scroll">
    <div class='checkbox' *ngFor="let option of _datasource">
      <label>
          <input type='checkbox' [(ngModel)]='option.checked' name='{{ctrlNameItem}}'  value='{{option.id}}' (change)='onChange($event)' /> {{ option.name }}
      </label>
    </div>
  </section>

  <select  *ngIf="enabledSelect2" class='form-control' id="{{_ctrlNameNumberId}}" multiple>
    <option *ngFor="let option of _datasource" value={{option.id}} selected>{{option.name}}</option>
  </select>`

})
export class MultiSelectComponent implements OnInit, OnDestroy {

  @Input() dataitem: string;
  @Input() datafilters: any;
  @Input() vm: ViewModel<any>
  @Input() endpoint: string;
  @Input() ctrlName: string;
  @Input() ctrlNameItem: string;
  @Input() type: string;
  @Input() disabledOnInit: boolean;
  @Input() enabledSelect2: boolean;
  @Input() fieldFilterName: any;
  @Input() filterBehavior: string;

  _datasource: any[];
  _modelOutput: any;
  _collectionjsonTemplate
  _modelInput: any;
  _filter: any;
  _notificationEmitter: Subscription;
  _numberId: number
  _ctrlNameNumberId: string;

  constructor(private api: ApiService<any>) {
    this.type = "filter";
    this._filter = {};
    this.enabledSelect2 = GlobalService.getGlobalSettings().enabledSelect2;
    this.fieldFilterName = "nome";
    this.filterBehavior = "GetDataItem"
  }

  ngOnInit() {

    this._numberId = Math.floor((Math.random() * 10000) + 1);
    this._ctrlNameNumberId = this.ctrlName + this._numberId.toString();

    if (!this.disabledOnInit) {
      this.init();
      this.getInstance();
    }

    this._notificationEmitter = GlobalService.getNotificationEmitter().subscribe((not) => {

      if (not.event == "edit" || not.event == "create" || not.event == "init") {
        this.init();
      }

      if (not.event == "change") {
        if (not.data.dataitem == this.dataitem)
          this.getInstance(not.data.parentFilter);
      }

    })
  }

  onChange(e) {
    this.updateValue(e.target.value, e.target.checked);
  }

  private init() {
    this._modelOutput = [];
    this._datasource = [];
    if (this.type.toLowerCase() == "filter") {
      if (this.vm.modelFilter[this.ctrlName]) {
        this._modelInput = this.transformeCSVModelFiltersInCollection(this.vm.modelFilter[this.ctrlName])
      }
    }
    else
      this._modelInput = this.vm.model[this.ctrlName];

    this._collectionjsonTemplate = "";
    $("#" + this._ctrlNameNumberId).val(null).trigger('change');
  }

  private transformeCSVModelFiltersInCollection(modelinputFilterCsv: string): any {
    let modelInputItems = [];
    if (modelinputFilterCsv) {
      let modelinputFilters = modelinputFilterCsv.split(',');
      for (var i = 0; i < modelinputFilters.length; i++) {
        let modelInputItem = {};
        modelInputItem[this.ctrlNameItem] = modelinputFilters[i];
        modelInputItems.push(modelInputItem)
      }
    }

    return modelInputItems;
  }

  private updateValue(value: any, checked: boolean) {

    this.addItem(value, checked);
    this.updateSerialize();

  }

  private updateSerialize() {

    if (this.type.toLowerCase() == "filter")
      return this.vm.modelFilter[this.ctrlName] = this.serializeToFilter();

    this.vm.model[this.ctrlName] = this.serializeToSave();
  }

  private addItems(values: any[]) {
    for (var i = 0; i < values.length; i++) {
      this.updateValue(values[i], true);
    }
  }

  private addItem(value: any, checked: boolean) {
    if (value) {
      if (checked) {
        this._modelOutput.push(value);
      }
      else {
        this._modelOutput = this._modelOutput.filter((item) => {
          return item != value;
        });
      }
    }
  }

  private serializeToSave() {

    let items: any = [];

    for (let item in this._modelOutput) {
      items.push(`{ "${this.ctrlNameItem}" : "${this._modelOutput[item]}"}`);
    }

    this._collectionjsonTemplate = `[ ${items.join()} ]`;

    return JSON.parse(this._collectionjsonTemplate);
  }

  private serializeToFilter() {
    return this._modelOutput.join()
  }

  private getInstance(parentFilter?: any) {

    let filters = Object.assign(this.datafilters || {}, parentFilter || {});
    if (this.enabledSelect2) {
      if (this._modelInput) {

        filters.ids = this._modelInput.map((item) => {
          return item[this.ctrlNameItem];
        });

        if (filters.ids.length > 0)
          this.getInstanceMultiSelect(filters);
      }
      this.getInstanceMultiSelect2(filters);
    }
    else
      this.getInstanceMultiSelect(filters);

  }

  private getInstanceMultiSelect2(filters: any) {

    let config = {
      ajax: this.api.setResource(this.dataitem, this.endpoint).getUrlConfig(true, this.fieldFilterName, this.filterBehavior, filters)
    }

    setTimeout(() => {
      $("#" + this._ctrlNameNumberId).select2(config).on('select2:select', (e) => {
        var selcteds = $("#" + this._ctrlNameNumberId).val();
        this._modelOutput = [];
        this.addItems(selcteds);
      }).on('select2:unselect', (e) => {
        var selcteds = $("#" + this._ctrlNameNumberId).val();
        if (selcteds.length > 0) {
          this._modelOutput = [];
          this.addItems(selcteds);
        }
        else {
          if (this.ctrlName) {
            this.vm.model[this.ctrlName] = null;
            this.vm.modelFilter[this.ctrlName] = null;
          }
        }
      });
    }, 100);

  }

  private getInstanceMultiSelect(filters: any) {

    this.api.setResource(this.dataitem, this.endpoint).getDataitem(filters).subscribe(result => {
      this._datasource = [];
      for (let item in result.dataList) {
        this._datasource.push({
          id: result.dataList[item].id,
          name: result.dataList[item].name,
          checked: this._modelInput ? this._modelInput.filter((selecteds) => {
            let checked = selecteds[this.ctrlNameItem] == result.dataList[item].id;
            if (checked)
              this.addItem(result.dataList[item].id, checked);
            return checked;
          }).length > 0 : false
        });
      }

    });


  }

  ngOnDestroy() {
    if (this._notificationEmitter)
      this._notificationEmitter.unsubscribe();
  }
}
