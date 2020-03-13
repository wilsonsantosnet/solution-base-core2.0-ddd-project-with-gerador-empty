
import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-sample-field-details',
    templateUrl: './sample-field-details.component.html',
    styleUrls: ['./sample-field-details.component.css']
})
export class SampleFieldDetailsComponent implements OnInit {


    @Input() vm: ViewModel<any>;

    constructor() { }

    ngOnInit() {

    }

}
