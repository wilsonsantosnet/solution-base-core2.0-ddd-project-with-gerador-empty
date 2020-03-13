import { Injectable } from '@angular/core';
import { CookieService } from './cookie.service';
import { LocalStorageService } from './local-storage.service';
import { ECacheType } from '../type-cache.enum';
//import { ECacheType } from 'app/common/type-cache.enum';



@Injectable()
export class CacheService {

    public static get(key: string, type: ECacheType) {
        if (type === ECacheType.COOKIE) {
            return CookieService.get(key);
        } else if (type === ECacheType.LOCAL) {
            return LocalStorageService.get(key);
        }
    }

    public static add(key: string, data: any, type: ECacheType, cookieExpiredDay?: number) {
        if (type === ECacheType.COOKIE) {
            CookieService.add(key, data, cookieExpiredDay);
        } else if (type === ECacheType.LOCAL) {
            LocalStorageService.add(key, data);
        }
    }

    public static update(key: string, data: any, type: ECacheType, cookieExpiredDay?: number) {
        if (type === ECacheType.COOKIE) {
            CookieService.remove(key);
            CookieService.add(key, data, cookieExpiredDay);
        } else if (type === ECacheType.LOCAL) {
            LocalStorageService.add(key, data);
        }
    }

    public static remove(key: string, type: ECacheType) {
        if (type === ECacheType.COOKIE) {
            CookieService.remove(key);
        } else if (type === ECacheType.LOCAL) {
            LocalStorageService.remove(key);
        }
    }

    public static removePartialKey(key: string, type: ECacheType) {
        if (type === ECacheType.COOKIE) {
            CookieService.removePartialKey(key);
        } else if (type === ECacheType.LOCAL) {
            LocalStorageService.remove(key);
        }
    }

    public static reset(type?: ECacheType) {
        if (type === ECacheType.COOKIE) {
            CookieService.reset();
        } else if (type === ECacheType.LOCAL) {
            LocalStorageService.reset();
        } else {
            CookieService.reset();
            LocalStorageService.reset();
        }
    }
}
