import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-sample-field-filter',
    templateUrl: './sample-field-filter.component.html',
    styleUrls: ['./sample-field-filter.component.css']
})
export class SampleFieldFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
