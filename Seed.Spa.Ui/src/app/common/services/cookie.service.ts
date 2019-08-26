import { Injectable } from '@angular/core';
import { Cookie } from 'ng2-cookies/ng2-cookies';

@Injectable()
export class CookieService {

    public static get(key: string) {
        return Cookie.get(key);
    }

    public static add(key: string, data: any, expiresTime?: number) {
        Cookie.set(key, data, expiresTime);
    }

    public static update(key: string, data: any) {
        Cookie.delete(key);
        Cookie.set(key, data);
    }

    public static remove(key: string) {
        Cookie.delete(key);
    }

    public static removePartialKey(key: string) {
        this.clearPartialCookies(key);
    }

    public static reset() {
        Cookie.delete("CURRENT_USER");
        Cookie.delete("TOKEN_AUTH");
        Cookie.delete("ARRAffinity");
    }

    public static clearAllCookies() {

        var cookies = document.cookie.split("; ");
        for (var c = 0; c < cookies.length; c++) {
            var d = window.location.hostname.split(".");
            while (d.length > 0) {
                var cookieBase = encodeURIComponent(cookies[c].split(";")[0].split("=")[0]) + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT; domain=' + d.join('.') + ' ;path=';
                var p = location.pathname.split('/');
                document.cookie = cookieBase + '/';
                while (p.length > 0) {
                    document.cookie = cookieBase + p.join('/');
                    p.pop();
                };
                d.shift();
            }
        }
    }

    public static clearPartialCookies(key: string) {

        var cookies = document.cookie.split("; ");
        for (var c = 0; c < cookies.length; c++) {
            var d = window.location.hostname.split(".");

            while (d.length > 0) {
                var cookieBase = encodeURIComponent(cookies[c].split(";")[0].split("=")[0]) + '=; expires=Thu, 01-Jan-1970 00:00:01 GMT; domain=' + d.join('.') + ' ;path=';
                if (cookieBase.startsWith(key)) {
                    var p = location.pathname.split('/');
                    document.cookie = cookieBase + '/';
                    while (p.length > 0) {
                        document.cookie = cookieBase + p.join('/');
                        p.pop();
                    };
                }
                d.shift();
            }
        }
    }

}
