import { Component, OnInit, Input, SecurityContext, Output, EventEmitter } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';


@Component({
  selector: 'app-avatar',
  template: `<img src="{{path}}" class="avatar-img" alt="{{vm.userName}}" />`
  //template: `<div class="avatar-img" [ngStyle]="{'background-image': san(vm.avatar)}"></div>`,
})
export class AvatarComponent implements OnInit {

  @Input() folderAvatar: any;
  @Input() vm: any;

  filter: string;
  path: string;
  constructor(private sanitizer: DomSanitizer) { }

  ngOnInit() {
    this.path = this.vm.downloadUri + "/" + this.folderAvatar + "/" + (this.vm.avatar || 'vazio.png');
  }

  san(fileName) {
    var _url = "url('" + this.vm.downloadUri + "/" + this.folderAvatar + "/" + (fileName || 'vazio.png') + "')";
    return this.sanitizer.sanitize(SecurityContext.HTML, _url)
  }
}
