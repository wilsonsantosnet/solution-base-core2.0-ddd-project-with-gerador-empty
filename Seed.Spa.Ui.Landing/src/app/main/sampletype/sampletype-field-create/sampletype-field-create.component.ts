import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { SampleTypeService } from '../sampletype.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-sampletype-field-create',
    templateUrl: './sampletype-field-create.component.html',
    styleUrls: ['./sampletype-field-create.component.css']
})
export class SampleTypeFieldCreateComponent implements OnInit {

   @Input() vm: ViewModel<any>;

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
