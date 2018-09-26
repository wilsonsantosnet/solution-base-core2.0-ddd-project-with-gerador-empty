
import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-tenant-field-details',
    templateUrl: './tenant-field-details.component.html',
    styleUrls: ['./tenant-field-details.component.css']
})
export class TenantFieldDetailsComponent implements OnInit {


    @Input() vm: ViewModel<any>;

    constructor() { }

    ngOnInit() {

    }

}
