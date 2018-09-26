import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';

import { ThemaService } from '../thema.service';
import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-thema-print',
    templateUrl: './thema-print.component.html',
    styleUrls: ['./thema-print.component.css'],
})
export class ThemaPrintComponent implements OnInit {

    vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private themaService: ThemaService, private route: ActivatedRoute) {
        this.vm = this.themaService.initVM();
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id)
        {
            this.themaService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            });
        }
        
        this.updateCulture();

    }
    
	updateCulture(culture: string = null) {
        this.themaService.updateCulture(culture).then((infos: any) => {
					this.vm.infos = infos;
					this.vm.grid = this.themaService.getInfoGrid(infos);
        });
        this.themaService.updateCultureMain(culture).then((infos: any) => {
					this.vm.generalInfo = infos;
        });
    }
    
    onPrint() {
        window.print();
    }
   


}
