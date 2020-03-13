import { Directive, ElementRef, Renderer, Input, Output, OnInit, EventEmitter } from '@angular/core';
import { NgModel } from '@angular/forms';

import { ApiService } from '../services/api.service';
import { GlobalService } from '../../global.service'


@Directive({
    selector: '[datasource-show]',
    providers: [NgModel]
})

export class DataSourceShowDirective implements OnInit {

    @Input() dataitem: string;
    @Input() endpoint: string;
    @Input() datafilters: any;

    constructor(private _elemetRef: ElementRef, private _renderer: Renderer, private api: ApiService<any>, private ngModel: NgModel) {

       
    }

    ngOnInit() {

        this.datasource(this._elemetRef.nativeElement);
        GlobalService.notification.subscribe((not) => {
            if (not.event == "create" || not.event == "edit") {
                this.datasource(this._elemetRef.nativeElement);
            }
            if (not.event == "change") {
                if (not.data.dataitem == this.dataitem)
                    this.datasource(this._elemetRef.nativeElement, not.data.parentFilter);
            }
        });
    }


    private datasource(el, parentFilter?: any) {

        var filters = Object.assign(this.datafilters || {}, parentFilter || {});
        this.api.setResource(this.dataitem, this.endpoint).getDataitem(filters).subscribe((result) => {

            if (result.dataList.length == 1) {
                el.style.display = 'none'
            }
           
        });

    }

}
