//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleTypeService } from '../sampletype.service';

@Component({
    selector: 'app-sampletype-container-edit',
    templateUrl: './sampletype-container-edit.component.html',
    styleUrls: ['./sampletype-container-edit.component.css'],
})
export class SampleTypeContainerEditComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private sampleTypeService: SampleTypeService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleTypeService.initVM();
    }

    ngOnInit() {

       
    }

}
