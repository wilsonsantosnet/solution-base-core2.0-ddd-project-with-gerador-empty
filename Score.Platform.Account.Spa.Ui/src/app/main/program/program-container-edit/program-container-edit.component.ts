//EXT
import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ProgramService } from '../program.service';

@Component({
    selector: 'app-program-container-edit',
    templateUrl: './program-container-edit.component.html',
    styleUrls: ['./program-container-edit.component.css'],
})
export class ProgramContainerEditComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private programService: ProgramService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.programService.initVM();
    }

    ngOnInit() {

       
    }

}
