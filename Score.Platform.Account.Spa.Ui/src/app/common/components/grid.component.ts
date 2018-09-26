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
          </thead>
          <tbody>
            <tr *ngFor="let item of vm.filterResult">

              <td class="text-center text-nowrap" *ngIf="showAction && actionLeft" >
                <button *ngFor="let btn of customButton" (click)="btn.click(item)" placement="top" title="{{btn.tooltip}}" class="btn btn-sm {{ btn.class }}">
                  <i class="fa {{ btn.icon }}"></i>
                </button>
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


  _modelOutput: any;
  _collectionjsonTemplate: any;
  _isCheckedAll: boolean;
  _isAsc: boolean;


  constructor() {
    this.init();
  }

  ngOnChanges(): void { }

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

}
