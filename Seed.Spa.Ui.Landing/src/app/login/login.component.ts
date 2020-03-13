import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../common/services/auth.service'
import { GlobalService } from '../global.service';

@Component({
    selector: 'app-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {


    typeLogin: string;
    sub: any;
    operation: string;

    constructor(private authService: AuthService, private route: ActivatedRoute) {
        this.typeLogin = GlobalService.getAuthSettings().TYPE_LOGIN;
        console.log(this.typeLogin);
    }

    ngOnInit() {

        this.sub = this.route.params.subscribe(params => {
            this.operation = params['operation'];
        });


        if (this.operation == "out") {
            console.log(this.operation);
            this.authService.logout();
        }
        else {
            this.authService.loginSso();
        }
    }

}
