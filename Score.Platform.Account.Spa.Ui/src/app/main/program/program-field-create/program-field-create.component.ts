import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ProgramService } from '../program.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-program-field-create',
    templateUrl: './program-field-create.component.html',
    styleUrls: ['./program-field-create.component.css']
})
export class ProgramFieldCreateComponent implements OnInit {

   @Input() vm: ViewModel<any>;

   constructor(private programService: ProgramService, private ref: ChangeDetectorRef) { }

   ngOnInit() {}


    ngOnChanges() {
       this.ref.detectChanges()
    }

    onSaveEnd(model: any) {
       
    }

    onBackEnd(model: any) {
       
    }

   


}
