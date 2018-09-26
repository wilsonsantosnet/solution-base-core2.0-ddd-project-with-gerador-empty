//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ThemaService } from '../thema.service';

@Component({
    selector: 'app-thema-container-edit',
    templateUrl: './thema-container-edit.component.html',
    styleUrls: ['./thema-container-edit.component.css'],
})
export class ThemaContainerEditComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private themaService: ThemaService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.themaService.initVM();
    }

    ngOnInit() {

       
    }

}
