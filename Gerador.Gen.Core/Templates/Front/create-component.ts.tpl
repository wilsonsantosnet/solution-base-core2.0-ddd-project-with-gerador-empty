import { Component, OnInit, Input, ChangeDetectorRef, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { <#className#>Service } from '../<#classNameLowerAndSeparator#>.service';
import { LocationHistoryService } from '../../../common/services/location.history';
import { ComponentBase } from "../../../common/components/component.base";
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-create',
    templateUrl: './<#classNameLowerAndSeparator#>-create.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-create.component.css'],
})
export class <#className#>CreateComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() vm: ViewModel<any>;
    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;
    @Output() saveEnd = new EventEmitter<any>();
    @Output() backEnd = new EventEmitter<any>();

 
    constructor(private <#classNameInstance#>Service: <#className#>Service, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {
        super();
        this.vm = null;
    }

    ngOnInit() {
        this.vm = this.<#classNameInstance#>Service.initVM();
        this.vm.actionDescription = "Cadastrar";

        this.<#classNameInstance#>Service.detectChanges(this.ref);  
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
            this.vm.model.<#KeyNameCamelCase#> = result.data.<#KeyNameCamelCase#>;
            this.saveEnd.emit();
            if (!this.vm.manterTelaAberta)
                this.router.navigate([LocationHistoryService.getLastNavigation(this.vm.key)]);
        });
    }

    onBack(e: any) {
        e.preventDefault();
        this.backEnd.emit();
    }

    ngOnDestroy() {
        this.<#classNameInstance#>Service.detectChangesStop();
    }
}
