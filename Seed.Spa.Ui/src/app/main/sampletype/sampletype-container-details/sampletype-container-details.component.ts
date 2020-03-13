//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleTypeService } from '../sampletype.service';

@Component({
    selector: 'app-sampletype-container-details',
    templateUrl: './sampletype-container-details.component.html',
    styleUrls: ['./sampletype-container-details.component.css'],
})
export class SampleTypeContainerDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private sampleTypeService: SampleTypeService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleTypeService.initVM();
    }

    ngOnInit() {

       
    }

}
