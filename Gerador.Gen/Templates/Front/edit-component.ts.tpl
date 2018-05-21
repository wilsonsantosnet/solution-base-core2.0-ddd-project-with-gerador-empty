import { Component, OnInit, Input,ChangeDetectorRef,OnDestroy } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';
import { GlobalService, NotificationParameters} from '../../../global.service';
import { LocationHistoryService } from '../../../common/services/location.history';
import { ComponentBase } from '../../../common/components/component.base';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-edit',
    templateUrl: './<#classNameLowerAndSeparator#>-edit.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-edit.component.css'],
})
export class <#className#>EditComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {
		super();
		this.vm = null;
    }

    ngOnInit() {

		this.vm = this.<#classNameInstance#>Service.initVM();
        this.vm.actionDescription = "Edição";

		this.<#classNameInstance#>Service.detectChanges(this.ref);

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        this.<#classNameInstance#>Service.get({ id: this.id }).subscribe((data) => {
            this.vm.model = data.data;
			this.showContainerEdit();
        })

   		this.updateCulture();
    }
	
	updateCulture(culture: string = null) {
        this.<#classNameInstance#>Service.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.<#classNameInstance#>Service.getInfoGrid(infos);
        });
    }

    onSave(model : any) {

        this.<#classNameInstance#>Service.save(model).subscribe((result) => {
            this.router.navigate([LocationHistoryService.getLastNavigation()])
        });
    }

	ngOnDestroy() {
		this.<#classNameInstance#>Service.detectChangesStop();
    }
}
