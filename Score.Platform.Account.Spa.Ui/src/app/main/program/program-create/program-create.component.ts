import { Component, OnInit, Input, ChangeDetectorRef, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ProgramService } from '../program.service';
import { LocationHistoryService } from '../../../common/services/location.history';
import { ComponentBase } from "../../../common/components/component.base";
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-program-create',
    templateUrl: './program-create.component.html',
    styleUrls: ['./program-create.component.css'],
})
export class ProgramCreateComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() vm: ViewModel<any>;
    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;
    @Output() saveEnd = new EventEmitter<any>();
    @Output() backEnd = new EventEmitter<any>();

 
    constructor(private programService: ProgramService, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {
        super();
        this.vm = null;
    }

    ngOnInit() {
        this.vm = this.programService.initVM();
        this.vm.actionDescription = "Cadastrar";

        this.programService.detectChanges(this.ref);  
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.programService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.programService.getInfoGrid(infos);
        });
    }

    onSave(model : any) {

            this.programService.save(model).subscribe((result) => {
            this.vm.model.programId = result.data.programId;
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
        this.programService.detectChangesStop();
    }
}
