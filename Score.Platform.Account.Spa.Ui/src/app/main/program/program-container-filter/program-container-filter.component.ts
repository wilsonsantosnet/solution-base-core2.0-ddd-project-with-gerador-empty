//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ProgramService } from '../program.service';

@Component({
    selector: 'app-program-container-filter',
    templateUrl: './program-container-filter.component.html',
    styleUrls: ['./program-container-filter.component.css'],
})
export class ProgramContainerFilterComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private programService: ProgramService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.programService.initVM();
    }

    ngOnInit() {

       
    }

}
