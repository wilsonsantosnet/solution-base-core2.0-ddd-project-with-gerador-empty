import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-program-field-filter',
    templateUrl: './program-field-filter.component.html',
    styleUrls: ['./program-field-filter.component.css']
})
export class ProgramFieldFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
