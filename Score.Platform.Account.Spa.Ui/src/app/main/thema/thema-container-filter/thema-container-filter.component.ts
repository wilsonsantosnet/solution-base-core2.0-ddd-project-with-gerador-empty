//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ThemaService } from '../thema.service';

@Component({
    selector: 'app-thema-container-filter',
    templateUrl: './thema-container-filter.component.html',
    styleUrls: ['./thema-container-filter.component.css'],
})
export class ThemaContainerFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private themaService: ThemaService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.themaService.initVM();
    }

    ngOnInit() {

       
    }

}
