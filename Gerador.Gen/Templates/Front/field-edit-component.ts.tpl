import { Component, OnInit, Input, ChangeDetectorRef } from '@angular/core';

import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-field-edit',
    templateUrl: './<#classNameLowerAndSeparator#>-field-edit.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-field-edit.component.css']
})
export class <#className#>FieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


    constructor(private <#classNameInstance#>Service: <#className#>Service, private ref: ChangeDetectorRef) { }

    ngOnInit() { }

	ngOnChanges() {
       this.ref.detectChanges()
	}

        
<#fieldItems#>
   
}
