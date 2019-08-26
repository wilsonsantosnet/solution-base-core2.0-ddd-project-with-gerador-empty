import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-details',
    templateUrl: './<#classNameLowerAndSeparator#>-details.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-details.component.css'],
})
export class <#className#>DetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute, private router: Router) {

        this.vm = this.<#classNameInstance#>Service.initVM();

    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id) {
            this.<#classNameInstance#>Service.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            })
        };
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.<#classNameInstance#>Service.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.<#classNameInstance#>Service.getInfoGrid(infos);
        });
    }

    
}
