//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleService } from '../sample.service';

@Component({
    selector: 'app-sample-container-create',
    templateUrl: './sample-container-create.component.html',
    styleUrls: ['./sample-container-create.component.css'],
})
export class SampleContainerCreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private sampleService: SampleService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleService.initVM();
    }

    ngOnInit() {

       
    }

}
