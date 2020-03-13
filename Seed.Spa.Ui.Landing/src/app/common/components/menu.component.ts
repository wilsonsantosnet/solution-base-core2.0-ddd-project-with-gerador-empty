import { Component, OnInit, Input, SecurityContext, Output, EventEmitter } from '@angular/core';
import { DomSanitizer } from '@angular/platform-browser';
import { AuthService } from '../../common/services/auth.service';

@Component({
    selector: 'app-menu',
    template: `
      <ul class="nav">
        <li class="nav-item" *ngFor="let item of vm.menu">
          <a class="nav-link" routerLink="{{item.Route}}">
            <i class="{{item.Icon}}"></i> {{item.Name}}
          </a>
        </li>
      </ul>`,
})
export class MenuComponent implements OnInit {

    @Output() onToggleMenu = new EventEmitter<any>();
    @Output() onLogout = new EventEmitter<any>();
    @Output() onFilter = new EventEmitter<any>();
    @Input() vm: any;
    @Input() folderAvatar: any;

    filter: string;

    constructor(private sanitizer: DomSanitizer, private authService: AuthService) { }

    

    ngOnInit() {
    }

    san(fileName) {
        var _url = "url('" + this.vm.downloadUri + "/" + this.folderAvatar + "/" + (fileName || 'vazio.png') + "')";
        return this.sanitizer.sanitize(SecurityContext.HTML, _url)
    }

    _onToggleMenu(event) {
        this.onToggleMenu.emit(event);
    }

    _onLogout(event) {
        this.onLogout.emit(event);
    }

    _onFilter(event) {
        this.onFilter.emit(event);
    }
}
