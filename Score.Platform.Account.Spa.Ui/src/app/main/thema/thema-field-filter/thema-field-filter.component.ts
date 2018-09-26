import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-thema-field-filter',
    templateUrl: './thema-field-filter.component.html',
    styleUrls: ['./thema-field-filter.component.css']
})
export class ThemaFieldFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
