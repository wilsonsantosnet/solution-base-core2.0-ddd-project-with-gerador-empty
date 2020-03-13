import { Component, OnInit, ViewChild, Output, EventEmitter, ChangeDetectorRef, OnDestroy, Input } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule, FormGroup, FormControl} from '@angular/forms';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { SampleService } from './sample.service';
import { ViewModel } from '../../common/model/viewmodel';
import { GlobalService, NotificationParameters} from '../../global.service';
import { ComponentBase } from '../../common/components/component.base';
import { LocationHistoryService } from '../../common/services/location.history';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-sample',
    templateUrl: './sample.component.html',
    styleUrls: ['./sample.component.css'],
    host: {
        '[class.className]': '_showClassName',
        '[class]': '_classNames'
    }
})
export class SampleComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;

    vm: ViewModel<any>;
	
    operationConfimationYes: any;
    changeCultureEmitter: Subscription;

    @ViewChild('filterModal', { static: false }) private filterModal: ModalDirective;
    @ViewChild('saveModal', { static: false }) private saveModal: ModalDirective;
    @ViewChild('editModal', { static: false }) private editModal: ModalDirective;
    @ViewChild('detailsModal', { static: false }) private detailsModal: ModalDirective;
    
    constructor(private sampleService: SampleService, private router: Router, private ref: ChangeDetectorRef) {
        super();
        this.vm = null;
    }

    ngOnInit() {

        this.vm = this.sampleService.initVM();
        this.configurationForParent();

        if (this.parentIdValue) 
            this.vm.modelFilter[this.parentIdField] = this.parentIdValue;

        this.onFilter(this.vm.modelFilter);

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
        this.sampleService.updateCulture(culture).then((infos : any) => {
            this.vm.infos = infos;
            this.vm.grid = this.sampleService.getInfoGrid(infos);
        });
        this.sampleService.updateCultureMain(culture).then((infos: any) => {
            this.vm.generalInfo = infos;
        });
    }


    public onFilter(modelFilter: any) {
        modelFilter.queryOptimizerBehavior = "GRID_Sample".toUpperCase();
        this.sampleService.get(modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
            this.filterModal.hide();
        })
    }

    public onExport() {
        this.sampleService.export(Object.assign(this.vm.modelFilter, { AttributeBehavior: "exportar" })).subscribe((result) => {
            var a = document.createElement("a");
            document.body.appendChild(a);
            (a as HTMLElement).style.visibility = 'hidden';

            var blob = new Blob([result._body], {
            	type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
            });
	    
			//Ou
			//var blob = new Blob([result], {
			//  type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet'
			//});

			var downloadUrl = window.URL.createObjectURL(blob);

            a.href = downloadUrl;
            a.download = "Sample.xlsx";
            a.click();
            window.URL.revokeObjectURL(downloadUrl);
        })
    }

    public onCreate() {

        this.showContainerCreate();

        this.vm.model = {};
        if (this.parentIdValue)
            this.vm.model[this.parentIdField] = this.parentIdValue;

        this.navigateStrategy(this.vm, this.saveModal, this.router, "/sample/create");
    }

    public onEdit(model: any) {

        this.vm.model = {};
        let newModel : any = model;
        if (model)  {
            newModel = { id: model.sampleId }
        }

        if (!this.vm.navigationModal) {
            this.navigateStrategy(this.vm, this.editModal, this.router, "/sample/edit/" + newModel.id);
        }
        else {
            this.sampleService.get(newModel).subscribe((result) => {
				      this.vm.model = result.dataList ? result.dataList[0] : result.data;
				      this.showContainerEdit();
				      this.editModal.show();
            })
        }
    }

    public onSave(model: any) {

        this.sampleService.save(model).subscribe((result) => {

             this.onFilter(this.vm.modelFilter);

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
            newModel = { id: model.sampleId }
        }
		
        if (!this.vm.navigationModal) {
            this.navigateStrategy(this.vm, this.editModal, this.router, "/sample/details/" + newModel.id);
        }
        else {
            this.sampleService.get(newModel).subscribe((result) => {
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
        this.router.navigate(['/sample/print', model.sampleId]);
    }

    public onDeleteConfimation(model: any) {

        var conf = GlobalService.operationExecutedParameters(
            "confirm-modal",
            () => {
                let newModel : any = model;
                if (model)
                {    
                    newModel = { id: model.sampleId }
                }
                this.sampleService.delete(newModel).subscribe((result) => {
                    this.vm.filterResult = this.vm.filterResult.filter(function (model) {
                        return  model.sampleId != result.data.sampleId;
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

        let modelFilter = this.sampleService.pagingConfig(this.vm.modelFilter, pageConfig);
        this.sampleService.get(modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
        });
    }

    public onOrderBy(order: any) {

        let modelFilter = this.sampleService.orderByConfig(this.vm.modelFilter, order);
        this.sampleService.get(modelFilter).subscribe((result) => {
            this.vm.filterResult = result.dataList;
            this.vm.summary = result.summary;
        });
    }

    ngOnDestroy() {
        if (this.changeCultureEmitter)
            this.changeCultureEmitter.unsubscribe();
        this.sampleService.detectChangesStop();
    }

}
