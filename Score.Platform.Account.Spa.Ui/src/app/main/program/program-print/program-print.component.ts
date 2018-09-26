import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';

import { ProgramService } from '../program.service';
import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-program-print',
    templateUrl: './program-print.component.html',
    styleUrls: ['./program-print.component.css'],
})
export class ProgramPrintComponent implements OnInit {

    vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private programService: ProgramService, private route: ActivatedRoute) {
        this.vm = this.programService.initVM();
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id)
        {
            this.programService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            });
        }
        
        this.updateCulture();

    }
    
	updateCulture(culture: string = null) {
        this.programService.updateCulture(culture).then((infos: any) => {
					this.vm.infos = infos;
					this.vm.grid = this.programService.getInfoGrid(infos);
        });
        this.programService.updateCultureMain(culture).then((infos: any) => {
					this.vm.generalInfo = infos;
        });
    }
    
    onPrint() {
        window.print();
    }
   


}
