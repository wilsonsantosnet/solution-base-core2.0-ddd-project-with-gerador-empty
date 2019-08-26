import { Component, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { GlobalService } from '../../global.service';

@Component({
    selector: 'loadingTop',
    template: `
    <div class="loader" [hidden]="!requesting">
        <img src="../../../assets/img/loader.gif" alt="carregando..." />
    </div>`,
    styles: [`
    .loader {
      opacity: .90;
      filter: alpha(opacity=90);
      }

    .loader img {
      width: 160px;
      heigth: 30px;
      margin: 10px;
     }
  `]
})
export class LoadingTopComponent implements OnInit, OnDestroy {


    requesting: boolean;
    _operationRequesting: EventEmitter<boolean>;

    constructor() {
        this._operationRequesting = new EventEmitter<boolean>();
    }

    ngOnInit() {
        this._operationRequesting = GlobalService.getOperationRequestingEmitter().subscribe((_requesting : boolean) => {
            this.requesting = _requesting;
        })
    }

    ngOnDestroy() {
        if (this._operationRequesting)
            this._operationRequesting.unsubscribe();
    }

}
