import { Component, OnInit, Input, ChangeDetectorRef, ViewChild } from '@angular/core';
import { ThemaService } from '../thema.service';

import { ViewModel } from '../../../common/model/viewmodel';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GlobalService, NotificationParameters } from '../../../global.service';

@Component({
    selector: 'app-thema-field-create',
    templateUrl: './thema-field-create.component.html',
    styleUrls: ['./thema-field-create.component.css']
})
export class ThemaFieldCreateComponent implements OnInit {

   @Input() vm: ViewModel<any>;

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
