import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-sampletype-field-filter',
    templateUrl: './sampletype-field-filter.component.html',
    styleUrls: ['./sampletype-field-filter.component.css']
})
export class SampleTypeFieldFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
