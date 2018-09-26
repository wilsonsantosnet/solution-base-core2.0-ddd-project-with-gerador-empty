import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { TenantService } from '../tenant.service';

@Component({
    selector: 'app-tenant-details',
    templateUrl: './tenant-details.component.html',
    styleUrls: ['./tenant-details.component.css'],
})
export class TenantDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private tenantService: TenantService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.tenantService.initVM();

    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id) {
            this.tenantService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            })
        };
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.tenantService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.tenantService.getInfoGrid(infos);
        });
    }

    
}
