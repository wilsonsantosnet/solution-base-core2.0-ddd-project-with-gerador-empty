import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { GlobalService } from '../global.service';
import { AuthService } from '../common/services/auth.service';

@Component({
  selector: 'app-unauthorized',
  templateUrl: './unauthorized.component.html',
  styleUrls: ['./unauthorized.component.css']
})
export class UnauthorizedComponent implements OnInit {

  vm: any = {};
  constructor(private authService: AuthService, private route: ActivatedRoute) {
    this.vm.actionTitle = "Access Unauthorized";
    this.vm.actionDescription = "Internal Server Error 401";
  }

  onLogin(e) {
    e.preventDefault();
    this.authService.logout();
  }

  ngOnInit() {

  }

}
