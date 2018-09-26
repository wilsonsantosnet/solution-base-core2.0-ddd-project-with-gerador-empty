//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ThemaService } from '../thema.service';

@Component({
    selector: 'app-thema-container-details',
    templateUrl: './thema-container-details.component.html',
    styleUrls: ['./thema-container-details.component.css'],
})
export class ThemaContainerDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private themaService: ThemaService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.themaService.initVM();
    }

    ngOnInit() {

       
    }

}
