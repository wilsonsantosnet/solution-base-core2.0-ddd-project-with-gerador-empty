
import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-program-field-details',
    templateUrl: './program-field-details.component.html',
    styleUrls: ['./program-field-details.component.css']
})
export class ProgramFieldDetailsComponent implements OnInit {


    @Input() vm: ViewModel<any>;

    constructor() { }

    ngOnInit() {

    }

}
