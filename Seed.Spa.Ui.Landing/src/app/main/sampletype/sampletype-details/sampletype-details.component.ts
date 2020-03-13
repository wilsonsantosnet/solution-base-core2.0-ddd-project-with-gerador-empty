import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { SampleTypeService } from '../sampletype.service';

@Component({
    selector: 'app-sampletype-details',
    templateUrl: './sampletype-details.component.html',
    styleUrls: ['./sampletype-details.component.css'],
})
export class SampleTypeDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private sampleTypeService: SampleTypeService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.sampleTypeService.initVM();

    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id) {
            this.sampleTypeService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            })
        };
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.sampleTypeService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.sampleTypeService.getInfoGrid(infos);
        });
    }

    
}
