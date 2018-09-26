
import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-thema-field-details',
    templateUrl: './thema-field-details.component.html',
    styleUrls: ['./thema-field-details.component.css']
})
export class ThemaFieldDetailsComponent implements OnInit {


    @Input() vm: ViewModel<any>;

    constructor() { }

    ngOnInit() {

    }

}
