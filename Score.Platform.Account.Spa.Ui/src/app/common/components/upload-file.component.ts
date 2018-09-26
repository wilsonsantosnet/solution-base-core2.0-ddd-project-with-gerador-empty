import { Component, OnInit, OnDestroy, Input, Output, EventEmitter, ChangeDetectorRef, ViewChild } from '@angular/core';
import { GlobalService, NotificationParameters } from "../../global.service";
import { ApiService } from "../../common/services/api.service";
import { ViewModel } from '../model/viewmodel';

@Component({
  selector: 'upload-custom',
  template: `
    <div class='row'>     
      <section class="col-md-12" [formGroup]="vm.form">
          <label>{{ label }}</label><br>
          <input type='file' accept="{{accept}}" #file name="{{ctrlName}}" hidden (change)="onChange($event)"/>
          <div class="input-group">
            <input type="text" readonly="readonly" [(ngModel)]='fileNameOld' class="form-control" placeholder="Selecionar arquivo..." formControlName="{{ctrlName}}" />
            <span class="btn-group">
              <button class="btn btn-secondary" (click)="file.click()" type="button" style='width:100px'>Procurar</button>
              <button class='btn btn-secondary' [hidden]="!fileName" type='button' (click)='onDelete()' style='width:100px'>Excluir</button>
            </span>
          </div>
          <hr *ngIf='fileName && isImage'>
          <div class="input-group" *ngIf='enableCopyLink'>
            <input *ngIf='fileName' type='text' id='{{fileName}}' value='{{downloadUri}}{{folder}}/{{fileName}}' class="form-control">
            <span class="btn-group">
              <button class='btn btn-secondary' [hidden]="!fileName" type='button' (click)='copyToClipboard(fileName)' style='width:100px'>Copy</button>
              <a class='btn btn-secondary' *ngIf='fileName' href='{{downloadUri}}{{folder}}/{{fileName}}' target='_blank' style='width:100px'>Ver</a>
            </span>
          </div>
          <hr *ngIf='fileName && enableCopyLink'>
          <img *ngIf='fileName && isImage' src='{{downloadUri}}{{folder}}/{{fileName}}' style='max-width:100%'  />
          <hr *ngIf='fileName && isImage'>
          <div ng-show='pasteArea' class='upload-component-paste-area upload-component-drop-area mt-2' id='upload-component-paste-area'>
          <p class='muted'>{{pasteAreaText}}<p>
          </div>
      </section>
    </div>`,
  providers: [ApiService],
})
export class UploadCustomComponent implements OnInit, OnDestroy {

  @ViewChild('file') fileUpload: any;
  @Output() onChangeUploadExternal = new EventEmitter<any>();

  @Input() label: string;
  @Input() accept: string;
  @Input() ctrlName: string;
  @Input() vm: ViewModel<any>
  @Input() folder: string;
  @Input() enabledUploadExternal: boolean;
  @Input() rename: boolean;
  @Input() pasteArea: boolean;
  @Input() pasteAreaText: string;
  @Input() enableCopyLink: boolean;

  fileName: string;
  fileNameOld: string;
  downloadUri: string;
  fileUri: string;
  isImage: boolean;

  _notificationEmitter: EventEmitter<NotificationParameters>;

  constructor(private api: ApiService<any>, private ref: ChangeDetectorRef) {

    this.downloadUri = GlobalService.getEndPoints().DOWNLOAD;
    this.fileUri = this.downloadUri + this.folder + "/" + this.fileName;
    this.enabledUploadExternal = false;
    this.accept = "*.*";
    this.rename = true;
    this.pasteArea = false;
    this.isImage = false;
    this.enableCopyLink = false;
    this.pasteAreaText = "Arraste e solte arquivos ou cole PrintScreens de telas";
    this._notificationEmitter = new EventEmitter<NotificationParameters>();

  }


  ngOnInit(): void {

    this.edit();

    this._notificationEmitter = GlobalService.getNotificationEmitter().subscribe((not: any) => {

      if (not.event == "edit") {
        this.edit();
      }

      if (not.event == "init") {
        this.fileNameOld = null;
        this.fileName = null;
      }
    })

    if (this.pasteArea) {

      let area = document.getElementById("upload-component-paste-area");
      
      area.addEventListener("paste", (e) => this.handlePaste(e));
      area.ondragover = function () { this.className = 'upload-component-paste-area'; return false; };
      area.ondrop = (e) => { this.handleDrop(e) }
      
    }

  }

  edit() {
    this.fileNameOld = this.vm.model[this.ctrlName];
    this.fileName = this.vm.model[this.ctrlName];
    this.verifyFileName(this.fileName)
  }

  copyToClipboard(file: any) {
    var copyText = document.getElementById(file) as any;
    copyText.select();
    document.execCommand("Copy");

  }

  handleDrop(e: any) {
    e.preventDefault();
    e.dataTransfer.files
    this.uploadFileOnPaste(e.dataTransfer.files[0]);
  }

  handlePaste(e: any) {

    for (var i = 0; i < e.clipboardData.items.length; i++) {
      var item = e.clipboardData.items[i];
      this.uploadFileOnPaste(item.getAsFile());
    }
  }

  uploadFileOnPaste(file: any) {

    this.fileNameOld = file.name;

    if (this.enabledUploadExternal)
      this.uploadCustom(file, this.rename);
    else
      this.uploadDefault(file, this.rename);

  }

  ngAfterViewChecked(): void {
    this.ref.detectChanges();
  }

  onChange(event: any) {

    if (event.target.files.length == 0)
      return false;

    let file: File = event.target.files[0];
    this.fileNameOld = file.name;

    if (this.enabledUploadExternal)
      return this.uploadCustom(file, this.rename);

    return this.uploadDefault(file, this.rename);
  }

  uploadCustom(file: File, rename: any) {
    this.onChangeUploadExternal.emit(file)
    this.vm.model[this.ctrlName] = file.name;
    this.fileName = file.name;
    this.pasteArea = false;
    this.verifyFileType(file);
    return true;
  }

  verifyFileType(file: File) {
    this.isImage = false;
    if (file.type == "image/png") this.isImage = true;
    if (file.type == "image/jpeg") this.isImage = true;
    if (file.type == "image/gif") this.isImage = true;
  }

  verifyFileName(fileName: string) {
    if (fileName) {
      this.isImage = false;
      if (fileName.endsWith("png")) this.isImage = true;
      if (fileName.endsWith("jpg")) this.isImage = true;
      if (fileName.endsWith("gif")) this.isImage = true;
    }
  }

  uploadDefault(file: File, rename: boolean) {

    this.api.setResource('upload').upload(file, this.folder, rename).subscribe(result => {
      this.vm.model[this.ctrlName] = result.data[0];
      this.fileName = result.data[0]
      this.pasteArea = false;
      this.verifyFileType(file);
    });
    return true;
  }

  onDelete() {
    this.api.setResource('upload').deleteUpload(this.folder, this.fileName).subscribe(() => {
      this.reset();
    });
  }

  reset() {
    this.fileUpload.nativeElement.value = '';
    this.vm.model[this.ctrlName] = null;
    this.fileName = null;
    this.fileNameOld = null;
    this.pasteArea = true;
  }

  ngOnChanges() {
    this.ref.detectChanges()
  }

  ngOnDestroy() {
    if (this._notificationEmitter)
      this._notificationEmitter.unsubscribe();
  }
}
