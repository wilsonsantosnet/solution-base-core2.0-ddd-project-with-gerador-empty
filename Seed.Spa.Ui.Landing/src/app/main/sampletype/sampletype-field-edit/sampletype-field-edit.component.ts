import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { SampleTypeService } from '../sampletype.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-sampletype-field-edit',
    templateUrl: './sampletype-field-edit.component.html',
    styleUrls: ['./sampletype-field-edit.component.css']
})
export class SampleTypeFieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


    constructor(private sampleTypeService: SampleTypeService, private ref: ChangeDetectorRef) { }

    ngOnInit() {}

    ngOnChanges() {
       this.ref.detectChanges()
    }

    onSaveEnd(model: any) {
       
    }

    onBackEnd(model: any) {
       
    }

        

   
}
