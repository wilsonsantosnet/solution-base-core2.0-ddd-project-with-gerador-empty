import { Component, OnInit, ViewChild, Output, EventEmitter, ChangeDetectorRef,OnDestroy } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule, FormGroup, FormControl} from '@angular/forms';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { SampleDashService } from './sampledash.service';
import { ViewModel } from '../../common/model/viewmodel';
import { GlobalService, NotificationParameters} from '../../global.service';
import { ComponentBase } from '../../common/components/component.base';
import { Subscription } from 'rxjs';

@Component({
    selector: 'app-sampledash',
    templateUrl: './sampledash.component.html',
    styleUrls: ['./sampledash.component.css'],
})
export class SampleDashComponent extends ComponentBase implements OnInit, OnDestroy {

    vm: ViewModel<any>;
    changeCultureEmitter: Subscription;

    
    constructor(private sampleDashService: SampleDashService, private router: Router, private ref: ChangeDetectorRef) {

        super();
        this.vm = null;
    }

    ngOnInit() {

        this.vm = this.sampleDashService.initVM();
        this.updateCulture();

        this.changeCultureEmitter = GlobalService.getChangeCultureEmitter().subscribe((culture : any) => {
            this.updateCulture(culture);
        });

    }

    updateCulture(culture: string = null)
    {
        this.sampleDashService.updateCulture(culture).then((infos : any) => {
            this.vm.infos = infos;
            this.vm.grid = this.sampleDashService.getInfoGrid(infos);
        });
    }

    ngOnDestroy() {
        this.changeCultureEmitter.unsubscribe();
        this.sampleDashService.detectChangesStop();
    }

}
