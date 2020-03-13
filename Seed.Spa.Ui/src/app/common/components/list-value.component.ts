import { Component, OnInit, Input, Output, EventEmitter, ChangeDetectorRef } from '@angular/core';

import { GlobalService } from '../../global.service';
import { ViewModel } from 'src/app/common/model/viewmodel';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from 'src/app/common/services/api.service';

@Component({
  selector: 'list-value',
  template: `   <section class="col-md-12">
                    <div class='form-group'>
                        <label class='mr-1'>{{ vm.infos | traduction:selectLabelTitle }}</label>
                        <select class='form-control' datasource name='selectDataItem' 
                            [(ngModel)]='formComponent[ctrlFieldKey]'  
                            [dataitem]="selectDataItem" 
                            [fieldFilterName]="selectFieldDescription"
                            [datafilters]="datafilters">
                        </select>
                    </div>
                </section>

                <section class="col-md-12">
                    <div class='form-group'>
                        <label class='mr-1'>{{ vm.infos | traduction:inputLabelTitle }}</label>
                        <div class="form-row">
                            <div class="col">
                                <input class="form-control" name="limitRuleValue" [(ngModel)]="formComponent[ctrlFieldValue]">
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
                                <th>{{selectLabelTitle}}</th>
                                <th>{{inputLabelTitle}}</th>
                                <th></th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr *ngFor="let item of this.vm.model[this.ctrlName]">
                                <th>{{item | navigationProperty: ctrlFieldDescription}}</th>
                                <td>{{item | navigationProperty: ctrlFieldValue}}</td>
                                <td><span class="text-danger cursor" (click)="removeItem(item[ctrlFieldValue])" title="{{ vm.infos | traduction:'Excluir' }}"><i class="fa fa-trash"></i></span></td>
                            </tr>
                        </tbody>
                    </table>
                </section>`
})
export class ListValueComponent implements OnInit {
  
    @Input() vm: ViewModel<any>;
    @Input() datafilters: any;

    @Input() ctrlName: string;
    @Input() ctrlFieldKey: string;
    @Input() ctrlFieldValue: string;
    @Input() ctrlFieldDescription: string;

    @Input() inputLabelTitle: string;
    @Input() selectLabelTitle: string;
    @Input() selectDataItem: string;
    @Input() selectFieldKey: string;
    @Input() selectFieldDescription: string;

    public formComponent: any;

    constructor(private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef, private api: ApiService<any>) {
        this.formComponent = {};
    }

    ngOnInit() {
        this.ctrlFieldDescription = this.ctrlFieldDescription || "description";
    }

    ngOnDestroy() {
        
    }

    addItem() { 
        if(this.isValid()) {
            let filter = Object.assign(this.datafilters || { });
            filter[this.selectFieldKey] = this.formComponent[this.ctrlFieldKey];

            this.api.setResource(this.selectDataItem).getDataitem(filter).subscribe((data) => {
                this.setValue(this.formComponent, this.ctrlFieldDescription, data.dataList[0].name);
                this.vm.model[this.ctrlName].push(Object.assign({}, this.formComponent));
                this.formComponent[this.ctrlFieldValue] = '';
            });
        }
    }

    removeItem(Valor) {
        this.vm.model[this.ctrlName] = this.vm.model[this.ctrlName].filter((obj) => {
            return obj[this.ctrlFieldValue] !== Valor;
        });
    }

    setValue(component, field, valor) {
        if(field.includes('.')) {   
            let node: any = {};
            for(let i = 0; i < field.split('.').length; i ++) {
                
                node = field.split('.')[i];

                if(component[node] == undefined) { 
                    component[node] = {}; 
                }

                if(i < field.split('.').length - 1) {
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
        if(this.formComponent[this.ctrlFieldKey] == undefined || this.formComponent[this.ctrlFieldKey] == '') {
            GlobalService.messageShow("Selecione um item no campo " + this.selectLabelTitle + ' para adicionar o registro');
            return false;
        }

        if(this.formComponent[this.ctrlFieldValue] == undefined || this.formComponent[this.ctrlFieldValue] == '') {
            GlobalService.messageShow("Informe o campo " + this.inputLabelTitle + ' para adicionar o registro');
            return false;
        }

        let item = this.vm.model[this.ctrlName].filter((obj) => {
            return obj[this.ctrlFieldValue] == this.formComponent[this.ctrlFieldValue];
        });

        if(item[0]) {
          GlobalService.messageShow("Item já foi adicionado na lista");
          return false;
        }

        return true;
    }
}
