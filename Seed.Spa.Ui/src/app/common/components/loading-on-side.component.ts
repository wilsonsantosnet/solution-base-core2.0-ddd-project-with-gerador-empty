import { Component, OnInit, OnDestroy, EventEmitter, Input } from '@angular/core';
import { GlobalService, OperationRequest } from '../../global.service';
import { Subscription } from 'rxjs';

@Component({
    selector: 'loading-on-side',
    template: `
    <h6 [hidden]="hiddenInfoData" class="text-muted"><i class="fa fa-warning"></i> Nenhum registro encontrado!<h6>
    <div class="loader" [hidden]="!requesting">
        <img src="../../../assets/img/loader-on-side.gif" alt="carregando..." />
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
export class LoadingOnSideComponent implements OnInit, OnDestroy {


    requesting: boolean;
    hiddenInfoData: boolean;
    operationRequesting: Subscription;
    @Input() resource: string;

    constructor() {
        this.hiddenInfoData = true;
    }

    ngOnInit() {
        this.operationRequesting = GlobalService.getOperationRequestingEmitter().subscribe((_requesting: OperationRequest) => {
            
            if (this.resource) {
                if (_requesting.resourceName.toLowerCase() == this.resource.toLowerCase()) {
                    this.requesting = _requesting.value;
                    this.hiddenInfoData = _requesting.count > 0;
                }
            }
        })
    }

    ngOnDestroy() {
        if (this.operationRequesting)
            this.operationRequesting.unsubscribe();
    }

}
