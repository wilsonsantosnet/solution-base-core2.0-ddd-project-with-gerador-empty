//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { TenantService } from '../tenant.service';

@Component({
    selector: 'app-tenant-container-filter',
    templateUrl: './tenant-container-filter.component.html',
    styleUrls: ['./tenant-container-filter.component.css'],
})
export class TenantContainerFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private tenantService: TenantService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.tenantService.initVM();
    }

    ngOnInit() {

       
    }

}
