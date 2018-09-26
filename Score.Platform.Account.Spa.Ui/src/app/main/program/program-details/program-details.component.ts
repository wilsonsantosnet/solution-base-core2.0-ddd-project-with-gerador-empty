import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ProgramService } from '../program.service';

@Component({
    selector: 'app-program-details',
    templateUrl: './program-details.component.html',
    styleUrls: ['./program-details.component.css'],
})
export class ProgramDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private programService: ProgramService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.programService.initVM();

    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id) {
            this.programService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            })
        };
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.programService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.programService.getInfoGrid(infos);
        });
    }

    
}
