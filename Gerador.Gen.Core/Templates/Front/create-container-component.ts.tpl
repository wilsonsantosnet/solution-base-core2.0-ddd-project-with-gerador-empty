//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-container-create',
    templateUrl: './<#classNameLowerAndSeparator#>-container-create.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-container-create.component.css'],
})
export class <#className#>ContainerCreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute, private router: Router) {

        this.vm = this.<#classNameInstance#>Service.initVM();
    }

    ngOnInit() {

       
    }

}
