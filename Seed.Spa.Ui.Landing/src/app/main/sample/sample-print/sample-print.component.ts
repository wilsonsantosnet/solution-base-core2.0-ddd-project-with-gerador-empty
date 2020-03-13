import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';

import { SampleService } from '../sample.service';
import { ViewModel } from '../../../common/model/viewmodel';

@Component({
    selector: 'app-sample-print',
    templateUrl: './sample-print.component.html',
    styleUrls: ['./sample-print.component.css'],
})
export class SamplePrintComponent implements OnInit {

    vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private sampleService: SampleService, private route: ActivatedRoute) {
        this.vm = this.sampleService.initVM();
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id)
        {
            this.sampleService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            });
        }
        
        this.updateCulture();

    }
    
	updateCulture(culture: string = null) {
        this.sampleService.updateCulture(culture).then((infos: any) => {
					this.vm.infos = infos;
					this.vm.grid = this.sampleService.getInfoGrid(infos);
        });
        this.sampleService.updateCultureMain(culture).then((infos: any) => {
					this.vm.generalInfo = infos;
        });
    }
    
    onPrint() {
        window.print();
    }
   


}
