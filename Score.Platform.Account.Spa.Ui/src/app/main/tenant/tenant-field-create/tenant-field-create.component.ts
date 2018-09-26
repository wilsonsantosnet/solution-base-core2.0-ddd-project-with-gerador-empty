import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { TenantService } from '../tenant.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-tenant-field-create',
    templateUrl: './tenant-field-create.component.html',
    styleUrls: ['./tenant-field-create.component.css']
})
export class TenantFieldCreateComponent implements OnInit {

   @Input() vm: ViewModel<any>;

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
