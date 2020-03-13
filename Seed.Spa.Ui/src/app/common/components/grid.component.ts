import { Component, OnInit, Input, Output, EventEmitter, OnChanges } from '@angular/core';

import { GlobalService } from '../../global.service';
import { ViewModel } from '../model/viewmodel';

@Component({
  selector: 'make-grid',
  template: `
    <div class="gc-table-responsive pre-scrollable">
        <table class="{{gridCss}}">
          <thead class="thead-inverse">
            <tr>

              <th width="1" class="text-center" *ngIf="showAction  && actionLeft">Ações</th>

              <th *ngFor="let grid of vm.grid" class="text-nowrap">
                <span class="table-sort">
                  {{ grid.info.label }}
                  <a *ngIf="showOrderBy" href='#' (click)='onOrderBy($event,grid.key)'><i class="fa fa-sort" aria-hidden="true"></i></a>
                </span>
              </th>

              <th width="1" class="text-center" *ngIf="showAction  && !actionLeft">Ações</th>

              <th width="65" class="text-center text-nowrap" *ngIf="showCheckbox">
                <input type="checkbox" class="grid-chk" [checked]='_isCheckedAll' (click)='onCheckAll($event)' />
              </th>
            </tr>
            <tr *ngIf="_fields && showFilters">
              <th width="1" class="text-center" *ngIf="showAction  && actionLeft"></th>
              <th *ngFor="let grid of _fields; let i=index" class="text-nowrap">
                  <span class="table-sort">
                    <input *ngIf="grid.show.value && grid.type == 'string'" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm"/>
                    <input *ngIf="grid.show.value && grid.type == 'DateTime'" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" datepicker class="form-control form-control-sm"/>
                    <input *ngIf="grid.show.value && grid.type == 'DateTime?'" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" datepicker class="form-control form-control-sm"/>
                    <input *ngIf="grid.show.value && grid.type == 'int?' && !grid.navigationProp" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm"/>
                    <input *ngIf="grid.show.value && grid.type == 'int' && !grid.navigationProp" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm"/>

                    <input *ngIf="grid.show.value && grid.type == 'decimal?' && !grid.navigationProp" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm" [textMask]='{ mask: vm.masks.maskDecimal }'/>
                    <input *ngIf="grid.show.value && grid.type == 'decimal' && !grid.navigationProp" type='text' [(ngModel)]="vm.modelFilter[grid.fieldName]"  (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm" [textMask]='{ mask: vm.masks.maskDecimal }'/>

                    <select *ngIf="grid.show.value && grid.navigationProp && grid.show.pagination" class='form-control' [(ngModel)]="vm.modelFilter[grid.fieldName]" datasource [dataitem]="grid.navigationProp" [fieldFilterName]="'Name'" (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm" [filterBehavior]="'GetDataListCustomPaging'"></select>
                    <select *ngIf="grid.show.value && grid.navigationProp && !grid.show.pagination" class='form-control' [(ngModel)]="vm.modelFilter[grid.fieldName]" datasource [dataitem]="grid.navigationProp" [fieldFilterName]="'Name'" (change)="onFilter($event,grid.fieldName)" class="form-control form-control-sm"></select>

                    <select *ngIf="grid.show.value && grid.type == 'dataitem'" class="form-control form-control-sm" [(ngModel)]="vm.modelFilter[grid.fieldName]" (change)="onFilter($event,grid.fieldName)" datasourceaux [dataitem]="grid.fieldName" [dataAux]="grid.aux" class="form-control form-control-sm"></select>
                    <select *ngIf="grid.show.value && grid.type == 'bool'"class="form-control form-control-sm" [(ngModel)]="vm.modelFilter[grid.fieldName]" (change)="onFilter($event,grid.fieldName)" datasourceaux [dataitem]="grid.fieldName" [dataAux]="[{ id: 'false', name: 'Não' }, { id: 'true', name: 'Sim' }]" ></select>
                    <select *ngIf="grid.show.value && grid.type == 'bool?'"class="form-control form-control-sm" [(ngModel)]="vm.modelFilter[grid.fieldName]" (change)="onFilter($event,grid.fieldName)" datasourceaux [dataitem]="grid.fieldName" [dataAux]="[{ id: 'false', name: 'Não' }, { id: 'true', name: 'Sim' }]" ></select>

                  </span>
              </th>
              <th width="1" class="text-center" *ngIf="showAction  && !actionLeft"></th>
              <th width="65" class="text-center text-nowrap" *ngIf="showCheckbox">
              </th>
            <tr>
          </thead>
          <tbody>
            <tr *ngFor="let item of vm.filterResult">

              <td class="text-center text-nowrap" *ngIf="showAction && actionLeft" >
                <span *ngIf="showCustom">
                  <button *ngFor="let btn of customButton" (click)="btn.click(item)" placement="top" title="{{btn.tooltip}}" class="btn btn-sm {{ btn.class }}">
                    <i class="fa {{ btn.icon }}"></i>
                  </button>
                </span> 
                <button (click)="onEdit($event, item)" *ngIf="showEdit" placement="top" title="Editar" class="btn btn-sm btn-primary">
                  <i class="icon icon-pencil"></i>
                </button>
                <button (click)="onDetails($event, item)" *ngIf="showDetails" placement="top" title="Detalhes" class="btn btn-sm btn-default">
                  <i class="icon icon-info"></i>
                </button>
                <button (click)="onPrint($event, item)" *ngIf="showPrint" placement="top" title="Imprimir" class="btn btn-sm btn-default">
                  <i class="icon icon-printer"></i>
                </button>
                <button (click)="onDeleteConfimation($event, item)" *ngIf="showDelete" placement="top" title="Excluir" class="btn btn-sm btn-danger">
                  <i class="icon icon-trash"></i>
                </button>
              </td>


              <td *ngFor="let grid of vm.grid" class="text-nowrap">
                <bind-custom [model]="bindFields(item, grid.key)" 
                             [format]="grid.info.type" 
                             [tag]="'span'"
                             [aux]="grid.info.aux"></bind-custom>
              </td>

              <td class="text-center text-nowrap" *ngIf="showAction && !actionLeft">
                <button *ngFor="let btn of customButton" (click)="btn.click(item)" placement="top" title="btn.tooltip" class="btn btn-sm {{ btn.class }}">
                  <i class="fa {{ btn.icon }}"></i>
                </button>
                <button (click)="onEdit($event, item)" *ngIf="showEdit" placement="top" title="Editar" class="btn btn-sm btn-primary">
                  <i class="fa fa-pencil"></i>
                </button>
                <button (click)="onDetails($event, item)" *ngIf="showDetails" placement="top" title="Detalhes" class="btn btn-sm btn-default">
                  <i class="fa fa-info"></i>
                </button>
                <button (click)="onPrint($event, item)" *ngIf="showPrint" placement="top" title="Imprimir" class="btn btn-sm btn-default">
                  <i class="fa fa-print"></i>
                </button>
                <button (click)="onDeleteConfimation($event, item)" *ngIf="showDelete" placement="top" title="Excluir" class="btn btn-sm btn-danger">
                  <i class="fa fa-trash-o"></i>
                </button>
              </td>
              
              <td class="text-center text-nowrap" *ngIf="showCheckbox">
                <input type="checkbox" class="grid-chk" name="gridCheckBox" [value]="item[checkboxProperty]" (change)='onChange($event)' />
              </td>

            </tr>
          </tbody>
        </table>`
})
export class MakeGridComponent implements OnChanges {

  @Input() vm: ViewModel<any>

  @Input() showCustom: boolean;
  @Input() showFilters: boolean;
  @Input() showEdit: boolean;
  @Input() showDetails: boolean;
  @Input() showPrint: boolean;
  @Input() showDelete: boolean
  @Input() showOrderBy: boolean;
  @Input() showCheckbox: boolean;
  @Input() showAction: boolean;
  @Input() actionLeft: boolean;
  @Input() gridCss: string;


  // [{ class: 'btn-success', tooltip: 'Configuracao', icon: 'fa-cog', click: (model) => { this.router.navigate(['/estagio/configuracao', model.estagioId]); } }]
  @Input() customButton: any = [];
  @Input() checkboxProperty: string;

  @Output() edit = new EventEmitter<any>();
  @Output() details = new EventEmitter<any>();
  @Output() print = new EventEmitter<any>();
  @Output() deleteConfimation = new EventEmitter<any>();
  @Output() orderBy = new EventEmitter<any>();
  @Output() filter = new EventEmitter<any>();


  _modelOutput: any;
  _collectionjsonTemplate: any;
  _isCheckedAll: boolean;
  _isAsc: boolean;
  _fields: any[];

  constructor() {
    this.init();
  }

  ngOnChanges(): void {

    if (this.vm.gridOriginal) {

      var map = this.vm.gridOriginal.map((item) => {
        return {
          fieldName: item.key,
          show: this.getShowFieldCustom(item),
          type: item.info.type,
          navigationProp: item.info.navigationProp,
          aux: item.info.aux,
        }
      });

      var _fields = [];
      for (var i = 0; i < this.vm.grid.filter(_ => _.info.list).length; i++) {

        var item = map.filter(_ => _.show.keyCustom == this.vm.grid[i].key)
        if (item.length > 0)
          _fields.push(item[0])
        else {
          _fields.push({
            fieldName: this.vm.grid[i].key,
            show: this.getShowFieldCustom(this.vm.grid[i]),
            type: this.vm.grid[i].info.type,
            navigationProp: this.vm.grid[i].info.navigationProp,
            aux: this.vm.grid[i].info.aux
          })
        }
      }

      this._fields = _fields;
    }

  }

  getShowFieldCustom(itemOriginal: any) {


    var externalFilter = false;
    if (itemOriginal.key.includes(".")) {
      if (!itemOriginal.key.endsWith("Id")) {
        externalFilter = true;
      }
    }

    var termA = itemOriginal.key.replace('Id', '.');
    var termB = itemOriginal.key.replace('sId', '.');

    var show = {
      type: "Default",
      term: termA,
      termOriginal: itemOriginal,
      ternFounded: null,
      value: false,
      keyCustom: null,
      pagination: false,
      customFilter: false,
    };


    var foundedResult = this.vm.grid.filter(_ => _.key.startsWith(termA));
    if (foundedResult.length > 0) {
      return {
        type: "foundedresultstartsWith",
        term: termA,
        termOriginal: itemOriginal,
        ternFounded: foundedResult[0],
        value: externalFilter ? foundedResult[0].info.customFilter : foundedResult[0].info.list,
        keyCustom: foundedResult[0].key,
        pagination: foundedResult[0].info.pagination,
        customFilter: foundedResult[0].info.customFilter
      };
    }

    var foundedResult = this.vm.grid.filter(_ => _.key.startsWith(termB));
    if (foundedResult.length > 0) {
      return {
        type: "foundedresultstartsWith",
        term: termB,
        termOriginal: itemOriginal,
        ternFounded: foundedResult[0],
        value: externalFilter ? foundedResult[0].info.customFilter : foundedResult[0].info.list,
        keyCustom: foundedResult[0].key,
        pagination: foundedResult[0].info.pagination,
        customFilter: foundedResult[0].info.customFilter
      };
    }

    var foundedResult = this.vm.grid.filter(_ => _.key == itemOriginal.key);
    if (foundedResult.length > 0) {
      return {
        type: "foundedresultEquals",
        term: itemOriginal,
        termOriginal: itemOriginal,
        ternFounded: foundedResult[0],
        value: externalFilter ? foundedResult[0].info.customFilter : foundedResult[0].info.list,
        keyCustom: foundedResult[0].key,
        pagination: foundedResult[0].info.pagination,
        customFilter: foundedResult[0].info.customFilter
      };
    }
    return show;
  }

  init() {
    this._modelOutput = [];
    this._collectionjsonTemplate = "";
    this._isCheckedAll = false;
    this._isAsc = true;
    this.showEdit = true;
    this.showDetails = true;
    this.showPrint = true;
    this.showDelete = true;
    this.showOrderBy = true;
    this.showCheckbox = false;
    this.showAction = true;
    this.actionLeft = GlobalService.getGlobalSettings().actionLeft;
    this.gridCss = "table table-striped table-app";
  }

  bindFields(item: any, key: any) {
    if (key.includes(".")) {
      let keys = key.split(".");
      if (keys.length == 2) return item[keys[0]] ? item[keys[0]][keys[1]] : "--";
      if (keys.length == 3) return item[keys[0]] && item[keys[0]][keys[1]] ? item[keys[0]][keys[1]][keys[2]] : "--";
    }
    return item[key];
  }

  onChange(evt: any) {

    this.addItem(parseInt(evt.target.value), evt.target.checked);

    this.vm.gridCheckModel = this.serializeToJson();

    let checkBoxItens = document.getElementsByName('gridCheckBox');

    for (var i = 0; i < checkBoxItens.length; i++) {
      if ((<HTMLInputElement>checkBoxItens[i]).checked == false) {
        this._isCheckedAll = false;
        break;
      }

      if (i == checkBoxItens.length - 1) {
        this._isCheckedAll = true;
      }
    }
  }

  onCheckAll(e: any) {

    this._isCheckedAll = e.target.checked;

    let checkBoxItens = document.getElementsByName('gridCheckBox');

    for (var i = 0; i < checkBoxItens.length; i++) {

      (<HTMLInputElement>checkBoxItens[i]).checked = e.target.checked;

      this.addItem(parseInt((<HTMLInputElement>checkBoxItens[i]).value), (<HTMLInputElement>checkBoxItens[i]).checked);
    }

    this.vm.gridCheckModel = this.serializeToJson();
  }

  private addItem(value: any, checked: boolean) {

    if (checked) {
      this._modelOutput.push(value);
    }
    else {
      this._modelOutput = this._modelOutput.filter((item: any) => {
        return item != value;
      });
    }
  }

  private serializeToJson() {

    this.removeDoubled();

    let items: any = [];

    for (let item in this._modelOutput) {
      items.push(`{ "${this.checkboxProperty}" : "${this._modelOutput[item]}"}`);
    }

    this._collectionjsonTemplate = `[ ${items.join()} ]`;

    return JSON.parse(this._collectionjsonTemplate);
  }

  private removeDoubled() {

    let modelOutputDuplicate = this._modelOutput;

    let modelOutputUnique = modelOutputDuplicate.filter(function (item: any, pos: any) {
      return modelOutputDuplicate.indexOf(item) == pos;
    });

    this._modelOutput = modelOutputUnique;
  }

  onEdit(evt: any, model: any) {
    evt.preventDefault();
    this.edit.emit(model);
  }

  onDetails(evt: any, model: any) {
    evt.preventDefault();
    this.details.emit(model);
  }

  onPrint(evt: any, model: any) {
    evt.preventDefault();
    this.print.emit(model);
  }

  onDeleteConfimation(evt: any, model: any) {
    evt.preventDefault();
    this.deleteConfimation.emit(model);
  }

  onOrderBy(evt: any, field: any) {
    this._isAsc = !this._isAsc
    evt.preventDefault();
    this.orderBy.emit({
      field: field,
      asc: this._isAsc
    });
  }
  onFilter(evt: any, field: any) {
    this.filter.emit(this.vm.modelFilter);
    console.log(field, this.vm.modelFilter);
  }
}
