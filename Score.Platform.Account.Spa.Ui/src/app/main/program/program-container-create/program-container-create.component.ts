//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ProgramService } from '../program.service';

@Component({
    selector: 'app-program-container-create',
    templateUrl: './program-container-create.component.html',
    styleUrls: ['./program-container-create.component.css'],
})
export class ProgramContainerCreateComponent implements OnInit {

    @Input() vm: ViewModel<any>;
  
    constructor(private programService: ProgramService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.programService.initVM();
    }

    ngOnInit() {

       
    }

}
