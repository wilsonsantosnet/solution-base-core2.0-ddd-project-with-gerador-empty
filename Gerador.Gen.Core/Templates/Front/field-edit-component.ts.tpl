import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-field-edit',
    templateUrl: './<#classNameLowerAndSeparator#>-field-edit.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-field-edit.component.css']
})
export class <#className#>FieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


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
