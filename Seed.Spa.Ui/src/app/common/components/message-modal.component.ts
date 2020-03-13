import { Component, OnInit, OnDestroy, Input, Output, EventEmitter, ViewChild } from '@angular/core';
import { ModalDirective } from 'ngx-bootstrap/modal';
import { GlobalService, NotificationParameters } from '../../global.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'message-modal',
  template: `
              <div bsModal #_messageModal="bs-modal" class="modal fade" >
                <div class="modal-dialog">
                  <div class="modal-content">
                    <div class="modal-header">
                      <h3 class="modal-title">Atenção</h3>
                    </div>
                    <div class="modal-body">
                      {{ vm.messageConfirmation }}
                    </div>
                    <div class="modal-footer">
                      <button class="btn btn-success" type="button" (click)="onMessageYes()">
                        <i class="icon-check icons"></i>
                        OK
                      </button>
                    </div>
                  </div>
                </div>
              </div>
            `
})
export class MessageModalComponent implements OnInit, OnDestroy {


  @ViewChild('_messageModal', { static: false }) private _messageModal: ModalDirective;

  vm: any;

  _notificationEmitter: Subscription;

  public config = {
    animated: true,
    keyboard: true,
    backdrop: true,
    ignoreBackdropClick: false
  };

  constructor() {
    this.vm = {};
    this.vm.messageConfirmation = "Atenção?"
  }


  ngOnInit() {

    this._notificationEmitter = GlobalService.getOperationExecutedEmitter().subscribe((result: any) => {
      if (result.selector == "message-modal") {
        this.vm.messageConfirmation = result.message;
        this.show();
      };
    })

  }

  show() {
    this._messageModal.show();
  }

  onMessageYes() {
    this._messageModal.hide();
  }

  onCancel() {
    this._messageModal.hide();
  }

  ngOnDestroy() {
    if (this._notificationEmitter)
      this._notificationEmitter.unsubscribe();
  }


}
