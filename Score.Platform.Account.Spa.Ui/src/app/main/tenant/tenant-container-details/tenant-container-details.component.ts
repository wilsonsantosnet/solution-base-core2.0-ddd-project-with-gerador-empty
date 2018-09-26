//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { TenantService } from '../tenant.service';

@Component({
    selector: 'app-tenant-container-details',
    templateUrl: './tenant-container-details.component.html',
    styleUrls: ['./tenant-container-details.component.css'],
})
export class TenantContainerDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private tenantService: TenantService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.tenantService.initVM();
    }

    ngOnInit() {

       
    }

}
