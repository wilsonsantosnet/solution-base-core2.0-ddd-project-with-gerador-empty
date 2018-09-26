import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ThemaService } from '../thema.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-thema-field-edit',
    templateUrl: './thema-field-edit.component.html',
    styleUrls: ['./thema-field-edit.component.css']
})
export class ThemaFieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


    constructor(private themaService: ThemaService, private ref: ChangeDetectorRef) { }

    ngOnInit() {}

    ngOnChanges() {
       this.ref.detectChanges()
    }

    onSaveEnd(model: any) {
       
    }

    onBackEnd(model: any) {
       
    }

        

   
}
