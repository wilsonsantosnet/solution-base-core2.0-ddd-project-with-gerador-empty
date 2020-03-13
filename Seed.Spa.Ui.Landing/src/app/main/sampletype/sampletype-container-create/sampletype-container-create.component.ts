//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleTypeService } from '../sampletype.service';

@Component({
    selector: 'app-sampletype-container-create',
    templateUrl: './sampletype-container-create.component.html',
    styleUrls: ['./sampletype-container-create.component.css'],
})
export class SampleTypeContainerCreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private sampleTypeService: SampleTypeService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleTypeService.initVM();
    }

    ngOnInit() {

       
    }

}
