import { Injectable, EventEmitter } from '@angular/core'
import { Routes } from '@angular/router';
import { CacheService } from './common/services/cache.service';
import { ECacheType } from './common/type-cache.enum';

export class GlobalService {

    static operationExecuted = new EventEmitter<OperationExecutedParameters>();
    static operationRequesting = new EventEmitter<OperationRequest>();
    static notification = new EventEmitter<NotificationParameters>();
    static changeCulture = new EventEmitter<string>();

    public static messageShow(message:string) {
        GlobalService.getOperationExecutedEmitter().emit(GlobalService.operationExecutedParameters("message-modal", null, message));
    }

    public static getNotificationEmitter() {

        if (!this.notification)
            this.notification = new EventEmitter<NotificationParameters>();
        return this.notification;
    }

    public static getOperationExecutedEmitter() {

        if (!this.operationExecuted)
            this.operationExecuted = new EventEmitter<OperationExecutedParameters>();
        return this.operationExecuted;
    }

    public static getOperationRequestingEmitter() {

        if (!this.operationRequesting)
            this.operationRequesting = new EventEmitter<OperationRequest>();
        return this.operationRequesting;
    }

    public static getChangeCultureEmitter() {

        if (!this.changeCulture)
            this.changeCulture = new EventEmitter<string>();
        return this.changeCulture;
    }

    public static getEndPoints() {

        if (!this._endpoint) {
            this._endpoint = new EndPoints();
            return this._endpoint;
        }

        return this._endpoint;
    }

    public static setAppsettings(config: any) {
        GlobalService.getEndPoints().setConfigSettings(config.ConfigSettings);
        GlobalService.getAuthSettings().setSSO(config.SSO);
    }

    public static getAuthSettings() {
        return new AuthSettings();
    }

    public static getGlobalSettings() {
        return new GlobalSettings();
    }

    public static operationExecutedParameters(_selector: string, _operation: any, _message: string = null) {
        return new OperationExecutedParameters(_selector, _operation, _message);
    }

    private static _endpoint: EndPoints;

};

export class OperationExecutedParameters {


    public selector: string;
    public operation: any;
    public message: string;

    constructor(_selector: string, _operation: any, _message: string) {

        this.selector = _selector;
        this.operation = _operation;
        this.message = _message;
    }

}

export class OperationRequest {

    resourceName: string;
    count: number;
    value: boolean;

    constructor(resourceName: string, count : number, value : boolean) {
        this.resourceName = resourceName;
        this.count = count;
        this.value = value;
    }
}

export class NotificationParameters {

    public event: string;
    public otherEvents: string[];
    public data?: any;

    constructor(_event: string, _data?: any, _otherEvents?: string[]) {

        this.event = _event;
        this.data = _data;
        this.otherEvents = _otherEvents || [""];

    }

}

export class EndPoints {

    public DEFAULT: string;
    public AUTHAPI: string;
    public AUTH: string;
    public APP: string;
    public DOWNLOAD: string;

    constructor() {


    }

    setConfigSettings(configSettings: any) {
        if (configSettings) {
            this.init(configSettings);
        }
    }

    init(configSettings: any) {

        this.DEFAULT = configSettings.DEFAULT;
        this.AUTHAPI = configSettings.AUTHAPI;
        this.AUTH = configSettings.AUTH;
        this.APP = configSettings.APP;
        this.DOWNLOAD = this.DEFAULT + "/document/download/";

    }

};
export class GlobalSettings {

    public enabledSelect2: boolean;
    public actionLeft: boolean;
    public CACHE_TYPE: ECacheType;
    public translateStrategy: any;
    public enabledOldBack: boolean;

    constructor() {
        this.enabledSelect2 = true;
        this.actionLeft = true;
        this.CACHE_TYPE = ECacheType.LOCAL;
        this.enabledOldBack = false;
        this.translateStrategy = {
            type: "SERVICE-FIELD",
        }
    }
}
export class AuthSettings {

    public TYPE_LOGIN: string;
    public CLIENT_ID: string;
    public CLIENT_ID_RO: string;
    public CLIENT_ID_CC: string;
    public SCOPE: string;
    public CACHE_TYPE: ECacheType;

    setSSO(sso: any) {
        if (sso) {
            this.init(sso);
        }
    }

    constructor() {
        this.TYPE_LOGIN = "SSO";
        this.CLIENT_ID = 'Seed-spa';
        this.CLIENT_ID_RO = 'Seed-spa';
        this.CLIENT_ID_CC = 'Seed-spa';
        this.SCOPE = "openid ssosa profile email";
        this.CACHE_TYPE = ECacheType.LOCAL;
    }

    init(sso: any) {
        this.CLIENT_ID = sso.CLIENT_ID;
        this.SCOPE = sso.SCOPE;
    }
};

