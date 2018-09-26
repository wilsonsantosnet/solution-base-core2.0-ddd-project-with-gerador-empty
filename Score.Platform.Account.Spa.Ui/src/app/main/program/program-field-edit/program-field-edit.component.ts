import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ProgramService } from '../program.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-program-field-edit',
    templateUrl: './program-field-edit.component.html',
    styleUrls: ['./program-field-edit.component.css']
})
export class ProgramFieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


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
