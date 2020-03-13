import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { SampleService } from '../sample.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';

@Component({
    selector: 'app-sample-field-edit',
    templateUrl: './sample-field-edit.component.html',
    styleUrls: ['./sample-field-edit.component.css']
})
export class SampleFieldEditComponent implements OnInit {

    @Input() vm: ViewModel<any>


    constructor(private sampleService: SampleService, private ref: ChangeDetectorRef) { }

    ngOnInit() {}

    ngOnChanges() {
       this.ref.detectChanges()
    }

    onSaveEnd(model: any) {
       
    }

    onBackEnd(model: any) {
       
    }

        

   
}
