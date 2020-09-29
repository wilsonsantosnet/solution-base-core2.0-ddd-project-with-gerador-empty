import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-field-create',
    templateUrl: './<#classNameLowerAndSeparator#>-field-create.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-field-create.component.css']
})
export class <#className#>FieldCreateComponent implements OnInit {

   @Input() vm: ViewModel<any>;

   constructor(private <#classNameInstance#>Service: <#className#>Service, private ref: ChangeDetectorRef) { }

   ngOnInit() {}


    ngOnChanges() {
       this.ref.detectChanges()
    }

    onSaveEnd(model: any) {
       
    }

    onBackEnd(model: any) {
       
    }

   
<#fieldItems#>

}
