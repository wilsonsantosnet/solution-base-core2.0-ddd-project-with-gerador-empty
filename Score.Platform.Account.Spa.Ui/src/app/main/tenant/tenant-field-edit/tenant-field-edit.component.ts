import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { TenantService } from '../tenant.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-tenant-field-edit',
    templateUrl: './tenant-field-edit.component.html',
    styleUrls: ['./tenant-field-edit.component.css']
})
export class TenantFieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


    constructor(private tenantService: TenantService, private ref: ChangeDetectorRef) { }

    ngOnInit() {}

    ngOnChanges() {
       this.ref.detectChanges()
    }

    onSaveEnd(model: any) {
       
    }

    onBackEnd(model: any) {
       
    }

        

   
}
