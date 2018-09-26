import { Component, OnInit, Input, ChangeDetectorRef, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { TenantService } from '../tenant.service';
import { LocationHistoryService } from '../../../common/services/location.history';
import { ComponentBase } from "../../../common/components/component.base";
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-tenant-create',
    templateUrl: './tenant-create.component.html',
    styleUrls: ['./tenant-create.component.css'],
})
export class TenantCreateComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() vm: ViewModel<any>;
    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;
    @Output() saveEnd = new EventEmitter<any>();
    @Output() backEnd = new EventEmitter<any>();

 
    constructor(private tenantService: TenantService, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {
        super();
        this.vm = null;
    }

    ngOnInit() {
        this.vm = this.tenantService.initVM();
        this.vm.actionDescription = "Cadastrar";

        this.tenantService.detectChanges(this.ref);  
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.tenantService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.tenantService.getInfoGrid(infos);
        });
    }

    onSave(model : any) {

            this.tenantService.save(model).subscribe((result) => {
            this.vm.model.tenantId = result.data.tenantId;
            this.saveEnd.emit();
            if (!this.vm.manterTelaAberta)
                this.router.navigate([LocationHistoryService.getLastNavigation(this.vm.key)]);
        });
    }

    onBack(e: any) {
        e.preventDefault();
        this.backEnd.emit();
    }

    ngOnDestroy() {
        this.tenantService.detectChangesStop();
    }
}
