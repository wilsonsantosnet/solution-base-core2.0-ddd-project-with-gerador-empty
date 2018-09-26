import { Component, OnInit } from '@angular/core';
import { AuthService } from './common/services/auth.service';
//import { AuthService } from 'app/common/services/auth.service'

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

        this.options = {
            position: ["top", "right"],
            timeOut: 5000,
            lastOnBottom: true

        }
    }


}
