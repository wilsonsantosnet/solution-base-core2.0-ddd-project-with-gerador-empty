//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { TenantService } from '../tenant.service';

@Component({
    selector: 'app-tenant-container-edit',
    templateUrl: './tenant-container-edit.component.html',
    styleUrls: ['./tenant-container-edit.component.css'],
})
export class TenantContainerEditComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private tenantService: TenantService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.tenantService.initVM();
    }

    ngOnInit() {

       
    }

}
