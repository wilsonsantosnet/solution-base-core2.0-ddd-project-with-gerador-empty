import { Component, OnInit, ViewChild, Output, EventEmitter, ChangeDetectorRef,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule, FormGroup, FormControl} from '@angular/forms';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { <#className#>Service } from './<#classNameLowerAndSeparator#>.service';
import { ViewModel } from '../../common/model/viewmodel';
import { GlobalService, NotificationParameters} from '../../global.service';
import { ComponentBase } from '../../common/components/component.base';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>',
    templateUrl: './<#classNameLowerAndSeparator#>.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>.component.css'],
})
export class <#className#>Component extends ComponentBase implements OnInit, OnDestroy {

    vm: ViewModel<any>;
    changeCultureEmitter: Subscription;

    
    constructor(private <#classNameInstance#>Service: <#className#>Service, private router: Router, private ref: ChangeDetectorRef) {

        super();
        this.vm = null;
    }

    ngOnInit() {

        this.vm = this.<#classNameInstance#>Service.initVM();
        this.updateCulture();

        this.changeCultureEmitter = GlobalService.getChangeCultureEmitter().subscribe((culture : any) => {
            this.updateCulture(culture);
        });

    }

    updateCulture(culture: string = null)
    {
        this.<#classNameInstance#>Service.updateCulture(culture).then((infos : any) => {
            this.vm.infos = infos;
            this.vm.grid = this.<#classNameInstance#>Service.getInfoGrid(infos);
        });
    }

    ngOnDestroy() {
        this.changeCultureEmitter.unsubscribe();
        this.<#classNameInstance#>Service.detectChangesStop();
    }

}
