import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';

import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';
import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-print',
    templateUrl: './<#classNameLowerAndSeparator#>-print.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-print.component.css'],
})
export class <#className#>PrintComponent implements OnInit {

    vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute) {
		this.vm = this.<#classNameInstance#>Service.initVM();
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

		if (this.id)
		{
			this.<#classNameInstance#>Service.get({ id: this.id }).subscribe((data) => {
				this.vm.details = data.data;
			});
		}

    }
    
	onPrint() {
        window.print();
	}
   


}
