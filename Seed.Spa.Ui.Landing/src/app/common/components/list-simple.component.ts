import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';

import { GlobalService } from '../../global.service';
import { ViewModel } from 'src/app/common/model/viewmodel';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/common/services/api.service';

@Component({
  selector: 'list-simple',
  template: `   <section class="col-md-12">
                    <div class="form-group">
                        <label class="mr-1">{{ vm.infos | traduction:selectLabelTitle }}</label> 
                        <div class="form-row">
                            <div class="col">
                                <select class="form-control" name="selectDataItem" 
                                    [(ngModel)]="formComponent[ctrlFieldKey]" datasource 
                                    [dataitem]="selectDataItem" 
                                    [fieldFilterName]="selectFieldDescription"
                                    [datafilters]="datafilters">
                                </select>
                            </div>
                            <div class="col-auto">
                                <button type="button" class="btn btn-sm btn-primary" (click)="addItem()"><i class="fa fa-plus"></i></button>
                            </div>
                        </div>
                    </div>
                    </section>

                    <section class="col-md-12">
                    <table class="table table-bordered table-hover table-stripped" *ngIf="this.vm.model[this.ctrlName] && this.vm.model[this.ctrlName].length > 0">
                        <colgroup>
                            <col>
                            <col width="1%">
                        </colgroup>
                        <thead class="thead-light">
                            <tr>
                                <th>{{selectLabelTitle}}</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr *ngFor="let item of this.vm.model[this.ctrlName]">
                                <th>{{item | navigationProperty: selectFieldDescription}}</th>
                                <td><span class="text-danger cursor" (click)="removeItem(item.description)" title="{{ vm.infos | traduction:'Excluir' }}"><i class="fa fa-trash"></i></span></td>
                            </tr>
                        </tbody>
                    </table>
                </section>`
})
export class ListSimpleComponent implements OnInit {

  @Input() vm: ViewModel<any>;
  @Input() datafilters: any;

  @Input() ctrlName: string;
  @Input() ctrlFieldKey: string;

  @Input() selectLabelTitle: string;
  @Input() selectDataItem: string;
  @Input() selectFieldKey: string;
  @Input() selectFieldDescription: string;

  public formComponent: any;

  constructor(private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef, private api: ApiService<any>) {
    this.formComponent = {};
  }

  ngOnInit() {
    if (!this.vm.model[this.ctrlName]) {
      this.vm.model[this.ctrlName] = [];
    }
  }

  ngOnDestroy() {

  }

  addItem() {
    if (this.validarCampos()) {
      let filter = Object.assign(this.datafilters || {});
      if (!filter) {
        filter = {};
      }

      filter[this.selectFieldKey] = this.formComponent[this.ctrlFieldKey];

      this.api.setResource(this.selectDataItem).getDataitem(filter).subscribe((data) => {
        let itemToAdd = {};
        this.setValue(itemToAdd, this.ctrlFieldKey, data.dataList[0].id);
        this.setValue(itemToAdd, this.selectFieldDescription, data.dataList[0].name);
        this.vm.model[this.ctrlName].push(Object.assign({}, itemToAdd));
      });
    }
  }

  removeItem(Valor) {
    if (this.vm.model[this.ctrlName]) {
      this.vm.model[this.ctrlName] = this.vm.model[this.ctrlName].filter((obj) => {
        return obj.description !== Valor;
      });
    }
  }

  validarCampos() {
    if (this.formComponent[this.ctrlFieldKey] == undefined || this.formComponent[this.ctrlFieldKey] == '') {
      GlobalService.messageShow("Selecione um item no campo " + this.selectLabelTitle + ' para adicionar o registro');
      return false;
    }

    if (this.vm.model[this.ctrlName]) {
      let item = this.vm.model[this.ctrlName].filter((obj) => {
        return obj[this.ctrlFieldKey] == this.formComponent[this.ctrlFieldKey];
      });

      if (item[0]) {
        GlobalService.messageShow("Item já foi adicionado na lista");
        return false;
      }
    }

    return true;
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


}
