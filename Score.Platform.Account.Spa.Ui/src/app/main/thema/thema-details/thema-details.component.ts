import { Component, OnInit, Input } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { ModalDirective } from 'ngx-bootstrap/modal';
import { ViewModel } from '../../../common/model/viewmodel';
import { ThemaService } from '../thema.service';

@Component({
    selector: 'app-thema-details',
    templateUrl: './thema-details.component.html',
    styleUrls: ['./thema-details.component.css'],
})
export class ThemaDetailsComponent implements OnInit {

    @Input() vm: ViewModel<any>;
    id: number;
    private sub: any;

    constructor(private themaService: ThemaService, private route: ActivatedRoute, private router: Router) {

        this.vm = this.themaService.initVM();

    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.id = params['id']; 
        });

        if (this.id) {
            this.themaService.get({ id: this.id }).subscribe((data) => {
                this.vm.details = data.data;
            })
        };
        this.updateCulture();
    }
    
    updateCulture(culture: string = null) {
        this.themaService.updateCulture(culture).then((infos: any) => {
            this.vm.infos = infos;
            this.vm.grid = this.themaService.getInfoGrid(infos);
        });
    }

    
}
