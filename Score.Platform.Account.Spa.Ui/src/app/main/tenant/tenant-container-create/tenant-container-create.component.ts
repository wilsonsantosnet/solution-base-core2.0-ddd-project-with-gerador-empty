//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { TenantService } from '../tenant.service';

@Component({
    selector: 'app-tenant-container-create',
    templateUrl: './tenant-container-create.component.html',
    styleUrls: ['./tenant-container-create.component.css'],
})
export class TenantContainerCreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private tenantService: TenantService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.tenantService.initVM();
    }

    ngOnInit() {

       
    }

}
