import { Component, OnInit, Input, ChangeDetectorRef, OnDestroy, Output, EventEmitter } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleTypeService } from '../sampletype.service';
import { LocationHistoryService } from '../../../common/services/location.history';
import { ComponentBase } from "../../../common/components/component.base";
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-sampletype-create',
    templateUrl: './sampletype-create.component.html',
    styleUrls: ['./sampletype-create.component.css'],
})
export class SampleTypeCreateComponent extends ComponentBase implements OnInit, OnDestroy {

    @Input() vm: ViewModel<any>;
    @Input() parentIdValue: any;
    @Input() parentIdField: string;
    @Input() isParent: boolean;
    @Output() saveEnd = new EventEmitter<any>();
    @Output() backEnd = new EventEmitter<any>();

 
    constructor(private sampleTypeService: SampleTypeService, private route: ActivatedRoute, private router: Router, private ref: ChangeDetectorRef) {
        super();
        this.vm = null;
    }

    ngOnInit() {
        this.vm = this.sampleTypeService.initVM();
        this.vm.actionDescription = "Cadastrar";

        this.sampleTypeService.detectChanges(this.ref);  
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.sampleTypeService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.sampleTypeService.getInfoGrid(infos);
        });
    }

    onSave(model : any) {

            this.sampleTypeService.save(model).subscribe((result) => {
            this.vm.model.sampleTypeId = result.data.sampleTypeId;
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
        this.sampleTypeService.detectChangesStop();
    }
}
