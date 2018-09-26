import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';

import { TenantService } from '../tenant.service';
import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-tenant-print',
    templateUrl: './tenant-print.component.html',
    styleUrls: ['./tenant-print.component.css'],
})
export class TenantPrintComponent implements OnInit {

    vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private tenantService: TenantService, private route: ActivatedRoute) {
        this.vm = this.tenantService.initVM();
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id)
        {
            this.tenantService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            });
        }
        
        this.updateCulture();

    }
    
	updateCulture(culture: string = null) {
        this.tenantService.updateCulture(culture).then((infos: any) => {
					this.vm.infos = infos;
					this.vm.grid = this.tenantService.getInfoGrid(infos);
        });
        this.tenantService.updateCultureMain(culture).then((infos: any) => {
					this.vm.generalInfo = infos;
        });
    }
    
    onPrint() {
        window.print();
    }
   


}
