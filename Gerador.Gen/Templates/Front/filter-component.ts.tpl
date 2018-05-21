import { Component, OnInit, Input } from '@angular/core';

import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-<#classNameLowerAndSeparator#>-filter',
    templateUrl: './<#classNameLowerAndSeparator#>-filter.component.html',
    styleUrls: ['./<#classNameLowerAndSeparator#>-filter.component.css']
})
export class <#className#>FilterComponent implements OnInit {

    @Input() vm: ViewModel<any>

    constructor() { }

    ngOnInit() {
    }

}
