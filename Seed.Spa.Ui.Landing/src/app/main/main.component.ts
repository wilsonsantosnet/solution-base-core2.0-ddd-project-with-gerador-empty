import { Component, OnInit, SecurityContext } from '@angular/core';
import { Router } from '@angular/router';
import { DomSanitizer } from '@angular/platform-browser';

import { AuthService } from '../common/services/auth.service'
import { GlobalServiceCulture } from '../global.service.culture'
import { GlobalService, NotificationParameters } from '../global.service'
import { MainService } from './main.service';
import { retry } from 'rxjs/operators';

declare var $: any;

@Component({
  selector: 'app-dashboard',
  templateUrl: './main.component.html',
  styleUrls: ['./main.component.scss'],
  providers: [GlobalServiceCulture, MainService]
})

export class MainComponent implements OnInit {

  vm: any;
  menuIsOpen: boolean;
  filter: string;

  constructor(private authService: AuthService, private globalServiceCulture: GlobalServiceCulture, private mainService: MainService, private sanitizer: DomSanitizer, private router: Router) {

    this.vm = {};
    this.menuIsOpen = true;
    this.vm.generalInfo = this.mainService.getInfosFields();
    this.vm.downloadUri = GlobalService.getEndPoints().DOWNLOAD;
    this.vm.avatar = null;

    this.mainService.updateCulture(this.vm);
    GlobalService.getChangeCultureEmitter().subscribe((culture: any) => {
      this.mainService.updateCulture(this.vm, culture);
    });
  }


  onUpdateCulture(event: any, culture: string) {

    event.preventDefault();
    this.mainService.updateCulture(this.vm, culture);
    GlobalService.getChangeCultureEmitter().emit(culture);

  }

  ngOnInit() {

    this.authService.getCurrentUser((result: any, firstTime: any) => {

      if (result.isAuth) {
        if (result.claims.name != null) {
          this.vm.userName = result.claims.name
        }

        if (result.claims.role != null) {
          this.vm.userRole = result.claims.role
        }

        if (result.claims.tools != null) {
          this.vm.menu = this.mainService.transformTools(result.claims.tools);
        }

        if (result.claims.avatar != null) {
          this.vm.avatar = result.claims.avatar
        }

        if (result.claims.typerole != null) {
          this.vm.typerole = result.claims.typerole
          this.vm.userRole = this.vm.userRole + "- [" + this.vm.typerole + "]";
        }

        if (firstTime)
          this.router.navigate(["/home"]);
      }
    });

    this.fixedHeader();

    // efeito de scroll top
    var windowH = $(window).height() / 2;
    $(window).on('scroll', function () {
      if ($(this).scrollTop() > windowH) {
        $("#myBtn").css('display', 'flex');
      } else {
        $("#myBtn").css('display', 'none');
      }
    });
    // efeito de scroll top
  }

  onToggleMenu(e: any) {

    this.menuIsOpen = !this.menuIsOpen
  }

  onLogout(e: any) {
    e.preventDefault();
    var conf = GlobalService.operationExecutedParameters("confirm-modal", () => {
      this.mainService.resetCulture();
      this.authService.logout();
    }, "Deseja Realmente Sair do Sistema");
    GlobalService.getOperationExecutedEmitter().emit(conf);
  }

  onFilter(filter: any) {

  }

  scrollToTop() {
    $('html, body').animate({ scrollTop: 0 }, 300);
  }

  fixedHeader() {
    /*[ Fixed Header ]
    ===========================================================*/
    var header = $('header');
    var logo = $(header).find('.logo img');
    var linkLogo1 = $(logo).attr('src');
    var linkLogo2 = $(logo).data('logofixed');

    $(window).on('scroll', function () {
      if ($(this).scrollTop() > 5 && $(this).width() > 992) {
        $(logo).attr('src', linkLogo2);
        $(header).addClass('header-fixed');
      }
      else {
        $(header).removeClass('header-fixed');
        $(logo).attr('src', linkLogo1);
      }
    });
  }

}
