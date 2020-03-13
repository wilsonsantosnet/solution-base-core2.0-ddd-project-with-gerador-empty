import { Directive, HostListener } from '@angular/core';
import { retry } from 'rxjs/operators';


@Directive({
  selector: '[appAsidebarToggler]'
})
export class AsidebarToggleDirective {
  constructor() { }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('aside-menu-lg-show');
    console.log("AsidebarToggleDirective");
  }
}

