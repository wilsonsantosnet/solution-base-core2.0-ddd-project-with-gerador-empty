import { Directive, HostListener } from '@angular/core';
import { retry } from 'rxjs/operators';


@Directive({
  selector: '[appSidebarToggler]'
})
export class SidebarToggleDirective {
  constructor() { }

  @HostListener('click', ['$event'])
  toggleOpen($event: any) {
    $event.preventDefault();
    document.querySelector('body').classList.toggle('sidebar-lg-show');
    document.querySelector('body').classList.toggle('sidebar-show');
    console.log("SidebarToggleDirective");
  }
}

