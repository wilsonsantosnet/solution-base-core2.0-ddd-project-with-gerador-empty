import { Component, OnInit, Input, ChangeDetectorRef, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ThemaService } from '../thema.service';
import { LocationHistoryService } from '../../../common/services/location.history';
import { ComponentBase } from "../../../common/components/component.base";
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-thema-create',
    templateUrl: './thema-create.component.html',
    styleUrls: ['./thema-create.component.css'],
})
export class ThemaCreateComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() vm: ViewModel<any>;
    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;
    @Output() saveEnd = new EventEmitter<any>();
    @Output() backEnd = new EventEmitter<any>();

 
    constructor(private themaService: ThemaService, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {
        super();
        this.vm = null;
    }

    ngOnInit() {
        this.vm = this.themaService.initVM();
        this.vm.actionDescription = "Cadastrar";

        this.themaService.detectChanges(this.ref);  
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.themaService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.themaService.getInfoGrid(infos);
        });
    }

    onSave(model : any) {

            this.themaService.save(model).subscribe((result) => {
            this.vm.model.themaId = result.data.themaId;
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
        this.themaService.detectChangesStop();
    }
}
