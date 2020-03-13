import { Component, OnInit, OnDestroy, EventEmitter } from '@angular/core';
import { GlobalService, OperationRequest } from '../../global.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'loading',
  template: `
    <div class="loader" [hidden]="!requesting">
        <img src="../../../assets/img/loader.gif" alt="carregando..." />
    </div>`,
  styles: [`
    .loader {
      position: fixed;
      z-index: 9999;
      background-color: #fff;
      opacity: .90;
      filter: alpha(opacity=90);
      width: 100%;
      height: 100%; }

    .loader img {
      display: block;
      margin: 200px auto; }
  `]
})
export class LoadingComponent implements OnInit, OnDestroy {


  requesting: boolean;
  operationRequesting: Subscription;

  ngOnInit() {

    this.operationRequesting = GlobalService.getOperationRequestingEmitter().subscribe((_requesting: OperationRequest) => {
      this.requesting = _requesting.value;
    })
  }

  constructor() { }

  ngOnDestroy() {
    if (this.operationRequesting)
      this.operationRequesting.unsubscribe();
  }

}
