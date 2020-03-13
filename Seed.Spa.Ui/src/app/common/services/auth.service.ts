import { Injectable } from '@angular/core';
import { Router, NavigationCancel } from '@angular/router';
import { URLSearchParams, } from '@angular/http';

import { ApiService } from '../services/api.service';
import { ECacheType } from '../type-cache.enum';
import { GlobalService } from '../../global.service';
import { CacheService } from '../services/cache.service';
import { StartupService } from '../../startup.service';

@Injectable()
export class AuthService {



    private readonly _nameToken: string;
    private readonly _nameEndPointAuthApi: string;
    private readonly _typeLogin: string;
    private readonly _authorizationUrl: string;
    private readonly _client_id: string;
    private readonly _client_id_ro: string;
    private readonly _client_id_cc: string;
    private readonly _redirect_uri: string;
    private readonly _response_type: string;
    private readonly _scope: string;
    private readonly _nameCurrentUser: string;
    private readonly _cacheType: ECacheType;
    private readonly _authorizationClaimsAddUrl: string;
    private readonly _authorizationAfterLogin: string;

    constructor(private apiAuth: ApiService<any>, private api: ApiService<any>, private router: Router, private startupService: StartupService) {



        this._nameToken = "TOKEN_AUTH";
        this._nameEndPointAuthApi = "AUTHAPI";
        this._typeLogin = GlobalService.getAuthSettings().TYPE_LOGIN;
        this._authorizationUrl = GlobalService.getEndPoints().AUTH + '/connect/authorize';
        this._authorizationClaimsAddUrl = GlobalService.getEndPoints().AUTH + '/AccountAfterAuth/ClaimsAdd';
        this._authorizationAfterLogin = GlobalService.getEndPoints().AUTH + '/Funnel/Register';
        this._client_id = GlobalService.getAuthSettings().CLIENT_ID;
        this._client_id_ro = GlobalService.getAuthSettings().CLIENT_ID_RO;
        this._client_id_cc = GlobalService.getAuthSettings().CLIENT_ID_CC;
        this._redirect_uri = GlobalService.getEndPoints().APP;
        this._response_type = "id_token token";
        this._scope = GlobalService.getAuthSettings().SCOPE;
        this._nameCurrentUser = "CURRENT_USER" + GlobalService.getAuthSettings().CLIENT_ID;
        this._cacheType = GlobalService.getAuthSettings().CACHE_TYPE;


    }
    
     public loginClientCredenciais(userName: any, password: any, callback = null) {

        this.apiAuth.enableNotification(false).setResource("authApp", GlobalService.getEndPoints().AUTHAPI).post({

          ClientId: this._client_id_cc,
          ClientSecret: "4aa288ca-1603-45c2-85c3-b41a08d2bd0a",
          Scope: this._scope,
          User: userName,
          Password: password

        }).subscribe(result => {

          console.log("<<<<< LOGIN CLIENT CREDENCIAL TOKEN >>>>>>", result.data);
          CacheService.add(this._nameToken, result.data.accessToken, this._cacheType);
          if (callback)
            callback(result.data.accessToken);

        }, err => { });
        this._typeLogin;
         
    }

    public loginResourceOwner(userName: any, password: any, reload = false) {

        this.apiAuth.setResource("auth", GlobalService.getEndPoints().AUTHAPI).post({

            ClientId: this._client_id_ro,
            ClientSecret: "secret",
            Scope: this._scope,
            User: userName,
            Password: password

        }).subscribe(result => {

            console.log("<<<<< LOGINRESOURCEOWNER TOKEN >>>>>>", result.data);
            CacheService.add(this._nameToken, result.data.accessToken, this._cacheType);
            this.router.navigate(["/home"]);

            if (reload)
                window.location.reload();

        }, err => { });

        this._typeLogin;
    }

    public loginAfterRegister(userName: any, password: any) {

        let state = Date.now() + "" + Math.random();
        localStorage["state"] = state;
        let token = CacheService.get(this._nameToken, this._cacheType)
        let url = this._authorizationAfterLogin + "?" +
            "userName=" + encodeURI(userName) + "&" +
            "password=" + encodeURI(password) + "&" +
            "client_id=" + encodeURI(this._client_id) + "&" +
            "redirect_uri=" + encodeURI(this._redirect_uri) + "&" +
            "response_type=" + encodeURI(this._response_type) + "&" +
            "scope=" + encodeURI(this._scope) + "&" +
            "state=" + encodeURI(state);

        CacheService.remove(this._nameCurrentUser, this._cacheType);

        window.location.href = url;
        return this._typeLogin;
    }

    public loginSso() {


        this.startupService.load();

        let state = Date.now() + "" + Math.random();
        localStorage["state"] = state;

        console.log("<<<<<< loginSso >>>>>", state, localStorage["state"]);

        var url =
            this._authorizationUrl + "?" +
            "client_id=" + encodeURI(this._client_id) + "&" +
            "redirect_uri=" + encodeURI(this._redirect_uri) + "&" +
            "response_type=" + encodeURI(this._response_type) + "&" +
            "scope=" + encodeURI(this._scope) + "&" +
            "state=" + encodeURI(state) + "&" +
            "nonce=xyz";

        window.location.href = url;
        return this._typeLogin;

    }

    public claimsAddLoginSso(claim_type: string, claim_value: string) {

        let state = Date.now() + "" + Math.random();
        localStorage["state"] = state;
        let token = CacheService.get(this._nameToken, this._cacheType)
        let url = this._authorizationClaimsAddUrl + "?" +
            "claims_type=" + encodeURI(claim_type) + "&" +
            "claims_value=" + encodeURI(claim_value) + "&" +
            "client_id=" + encodeURI(this._client_id) + "&" +
            "redirect_uri=" + encodeURI(this._redirect_uri) + "&" +
            "response_type=" + encodeURI(this._response_type) + "&" +
            "scope=" + encodeURI(this._scope) + "&" +
            "state=" + encodeURI(state);

        CacheService.remove(this._nameCurrentUser, this._cacheType);

        window.location.href = url;
        return this._typeLogin;
    }

    public getTypeLogin() {
        return this._typeLogin;
    }

    public logout() {

        this.reset();

        if (this._typeLogin == "SSO") {
            var authorizationUrl = GlobalService.getEndPoints().AUTH + 'account/logout?returnUrl=' + GlobalService.getEndPoints().APP + "/login";
            window.location.href = authorizationUrl;
        }
        else {
            this.router.navigate(["/login"]);
        }
    }

    public processTokenCallback() {

        if (window.location.href.indexOf("access_token") > -1) {

            let hash = window.location.hash.substr(1);

            let result = hash.split('&').reduce(function (result: any, item: any) {
                let parts = item.split('=');
                result[parts[0]] = parts[1];
                return result;
            }, {}) as any;


            if (!result.error) {
                if (result.state !== localStorage["state"]) {
                    console.log("<<<<< INVALID STATE >>>>>>", result.state, localStorage["state"]);
                    localStorage.removeItem("state");
                    this.router.navigate(["/login"]);
                }
                else {
                    console.log("<<<<< VALID STATE >>>>>>", result.state, localStorage["state"]);
                    console.log("<<<<< TOKEN >>>>>>", result.access_token);
                    console.log("<<<<< ENDPOINTS >>>>>>", GlobalService.getEndPoints());
                    localStorage.removeItem("state");
                    this._acceptlogin(result.access_token, false)
                }
            }
        }

    }

    public getCurrentUser(callback: any) {

        var currentUser = this.currentUser();
        if (currentUser.isAuth)
            callback(currentUser, false);
        else {
            this.api.setResource('CurrentUser').enableNotification(false).get().subscribe(data => {
                CacheService.add(this._nameCurrentUser, JSON.stringify(data.data), this._cacheType);
                callback(this.currentUser(), true);
            }, err => {
                this.loginSso();
            });
        }
    }

    public currentUser() {
        var currentUser = CacheService.get(this._nameCurrentUser, this._cacheType);
        return {
            isAuth: currentUser ? true : false,
            claims: JSON.parse(currentUser)
        }
    }
    
    public IsAuthApiVerify() {
        return this.api.setResource('CurrentUser/isAuth').enableNotification(false).get();
    }

    public isAuthenticated(): boolean {
        const token = CacheService.get(this._nameToken, this._cacheType);
        return token !== null;
    }

    private _acceptlogin(token: any, reload: any) {

        console.log("<<<<<<< _acceptlogin >>>>>>>>>>", token)
        CacheService.add(this._nameToken, token, this._cacheType);

        this.router.navigate(["/home"]);

        if (reload)
            window.location.reload();
    }

    reset() {
        CacheService.reset(this._cacheType);

    }

    private makeUrl(url: any, noCache = false) {

        if (noCache)
            return url;

        return url + '?v=' + Math.random();
    }

}
