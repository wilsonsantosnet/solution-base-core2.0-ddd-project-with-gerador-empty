
import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-sampletype-field-details',
    templateUrl: './sampletype-field-details.component.html',
    styleUrls: ['./sampletype-field-details.component.css']
})
export class SampleTypeFieldDetailsComponent implements OnInit {


    @Input() vm: ViewModel<any>;

    constructor() { }

    ngOnInit() {

    }

}
