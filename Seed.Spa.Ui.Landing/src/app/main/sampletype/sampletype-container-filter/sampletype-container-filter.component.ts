//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleTypeService } from '../sampletype.service';

@Component({
    selector: 'app-sampletype-container-filter',
    templateUrl: './sampletype-container-filter.component.html',
    styleUrls: ['./sampletype-container-filter.component.css'],
})
export class SampleTypeContainerFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private sampleTypeService: SampleTypeService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleTypeService.initVM();
    }

    ngOnInit() {

       
    }

}
