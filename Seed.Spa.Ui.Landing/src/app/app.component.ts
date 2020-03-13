import { Component, OnInit } from '@angular/core';
import { AuthService } from './common/services/auth.service';
//import { AuthService } from 'app/common/services/auth.service'

declare var $: any;

@Component({
    selector: 'body',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {

    options: any;
    constructor(private authService: AuthService) {

    }

    ngOnInit() {
        this.authService.processTokenCallback();
        $('.parallax100').parallax100();
    }


}
