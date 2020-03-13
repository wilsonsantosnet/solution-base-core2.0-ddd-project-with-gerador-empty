//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleService } from '../sample.service';

@Component({
    selector: 'app-sample-container-details',
    templateUrl: './sample-container-details.component.html',
    styleUrls: ['./sample-container-details.component.css'],
})
export class SampleContainerDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private sampleService: SampleService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleService.initVM();
    }

    ngOnInit() {

       
    }

}
