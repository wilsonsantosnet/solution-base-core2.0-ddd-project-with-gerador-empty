import { Component, OnInit, Input, ChangeDetectorRef} from '@angular/core';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

import { ViewModel } from '../../../common/model/viewmodel';

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

   
<#fieldItems#>

}
