import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-tenant-field-filter',
    templateUrl: './tenant-field-filter.component.html',
    styleUrls: ['./tenant-field-filter.component.css']
})
export class TenantFieldFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
