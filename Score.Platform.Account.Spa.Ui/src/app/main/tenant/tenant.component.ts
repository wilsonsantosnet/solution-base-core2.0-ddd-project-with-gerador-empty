import { Component, OnInit, ViewChild, Output, EventEmitter, ChangeDetectorRef, OnDestroy, Input } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule, FormGroup, FormControl} from '@angular/forms';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { TenantService } from './tenant.service';
import { ViewModel } from '../../common/model/viewmodel';
import { GlobalService, NotificationParameters} from '../../global.service';
import { ComponentBase } from '../../common/components/component.base';
import { LocationHistoryService } from '../../common/services/location.history';

@Component({
    selector: 'app-tenant',
    templateUrl: './tenant.component.html',
    styleUrls: ['./tenant.component.css'],
})
export class TenantComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;

    vm: ViewModel<any>;
	
    operationConfimationYes: any;
    changeCultureEmitter: EventEmitter<string>;

    @ViewChild('filterModal') private filterModal: ModalDirective;
    @ViewChild('saveModal') private saveModal: ModalDirective;
    @ViewChild('editModal') private editModal: ModalDirective;
    @ViewChild('detailsModal') private detailsModal: ModalDirective;
    
    constructor(private tenantService: TenantService, private router: Router, private ref: ChangeDetectorRef) {

        super();
        this.vm = null;

    }

    ngOnInit() {

        this.vm = this.tenantService.initVM();
        this.configurationForParent();

        if (this.parentIdValue) 
            this.vm.modelFilter[this.parentIdField] = this.parentIdValue;

        this.tenantService.detectChanges(this.ref);
        this.tenantService.OnHide(this.saveModal, this.editModal, () => { this.hideComponents() });

        this.tenantService.get(this.vm.modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
        });

        this.updateCulture();

        this.changeCultureEmitter = GlobalService.getChangeCultureEmitter().subscribe((culture : any) => {
            this.updateCulture(culture);
        });

    
        this.vm.isParent = this.isParent;
        this.vm.ParentIdField = this.parentIdField;
    }

    configurationForParent() {
        if (this.isParent) {
            this._showBtnBack = false;
            this._showBtnFilter = false;
            this._showBtnDetails = false;
            this._showBtnPrint = false;
        }
    }

    updateCulture(culture: string = null)
    {
        this.tenantService.updateCulture(culture).then((infos : any) => {
            this.vm.infos = infos;
            this.vm.grid = this.tenantService.getInfoGrid(infos);
        });
        this.tenantService.updateCultureMain(culture).then((infos: any) => {
            this.vm.generalInfo = infos;
        });
    }


    public onFilter(modelFilter: any) {

        this.tenantService.get(modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
            this.filterModal.hide();
        })
    }

    public onExport() {
        this.tenantService.export().subscribe((result) => {
            var blob = new Blob([result._body], {
                type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            });
            var downloadUrl = window.URL.createObjectURL(blob);
            window.location.href = downloadUrl;
        })
    }

    public onCreate() {

        this.showContainerCreate();

        this.vm.model = {};
        if (this.parentIdValue)
            this.vm.model[this.parentIdField] = this.parentIdValue;

        this.navigateStrategy(this.vm, this.saveModal, this.router, "/tenant/create");
    }

    public onEdit(model: any) {

        this.vm.model = {};
        let newModel : any = model;
        if (model)  {
            newModel = { id: model.tenantId }
        }

        if (!this.vm.navigationModal) {
            this.navigateStrategy(this.vm, this.editModal, this.router, "/tenant/edit/" + newModel.id);
        }
        else {
            this.tenantService.get(newModel).subscribe((result) => {
				      this.vm.model = result.dataList ? result.dataList[0] : result.data;
				      this.showContainerEdit();
				      this.editModal.show();
            })
        }
    }

    public onSave(model: any) {

        this.tenantService.save(model).subscribe((result) => {

            this.vm.filterResult = this.vm.filterResult.filter(function (model) {
                return  model.tenantId != result.data.tenantId;
            });

            this.vm.model.tenantId = result.data.tenantId;
            this.vm.filterResult.push(result.data);
            this.vm.summary.total = this.vm.filterResult.length

            if (!this.vm.manterTelaAberta) {
                this.saveModal.hide();
                this.editModal.hide();
                this.hideContainerCreate();
                this.hideContainerEdit();
            }

        });

    }

    public onDetails(model: any) {
    
        this.vm.details = {};
        let newModel : any = model;
        if (model)
        {
            newModel = { id: model.tenantId }
        }
		
        if (!this.vm.navigationModal) {
            this.navigateStrategy(this.vm, this.editModal, this.router, "/tenant/details/" + newModel.id);
        }
        else {
            this.tenantService.get(newModel).subscribe((result) => {
				      this.vm.details = result.dataList ? result.dataList[0] : result.data;
				      this.showContainerDetails();
				      this.detailsModal.show();
            })
        }

    }

    public onCancel() {

        this.saveModal.hide();
        this.editModal.hide();
        this.detailsModal.hide();
        this.filterModal.hide();
        this.hideComponents();
    }

    public onShowFilter() {
        this.showContainerFilters();
        this.filterModal.show();
    }

    public onClearFilter() {
        this.vm.modelFilter = {};
        GlobalService.getNotificationEmitter().emit(new NotificationParameters("init", {
            model: {}
        }));
    }

    public onPrint(model: any) {
        this.router.navigate(['/tenant/print', model.tenantId]);
    }

    public onDeleteConfimation(model: any) {

        var conf = GlobalService.operationExecutedParameters(
            "confirm-modal",
            () => {
                let newModel : any = model;
                if (model)
                {    
                    newModel = { id: model.tenantId }
                }
                this.tenantService.delete(newModel).subscribe((result) => {
                    this.vm.filterResult = this.vm.filterResult.filter(function (model) {
                        return  model.tenantId != result.data.tenantId;
                    });
                    this.vm.summary.total = this.vm.filterResult.length
                });
            }
        );

        GlobalService.getOperationExecutedEmitter().emit(conf);
    }

    public onConfimationYes() {
        this.operationConfimationYes();
    }

    public onPageChanged(pageConfig: any) {

        let modelFilter = this.tenantService.pagingConfig(this.vm.modelFilter, pageConfig);
        this.tenantService.get(modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
        });
    }

    public onOrderBy(order: any) {

        let modelFilter = this.tenantService.orderByConfig(this.vm.modelFilter, order);
        this.tenantService.get(modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
        });
    }

    ngOnDestroy() {
        this.changeCultureEmitter.unsubscribe();
        this.tenantService.detectChangesStop();
    }

}
