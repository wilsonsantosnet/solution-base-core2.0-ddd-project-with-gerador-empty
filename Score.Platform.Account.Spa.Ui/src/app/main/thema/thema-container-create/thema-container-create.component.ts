//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ThemaService } from '../thema.service';

@Component({
    selector: 'app-thema-container-create',
    templateUrl: './thema-container-create.component.html',
    styleUrls: ['./thema-container-create.component.css'],
})
export class ThemaContainerCreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private themaService: ThemaService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.themaService.initVM();
    }

    ngOnInit() {

       
    }

}
