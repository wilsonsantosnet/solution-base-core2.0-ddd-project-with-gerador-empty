import { Injectable } from '@angular/core';

@Injectable()
export class LocalStorageService {

    public static get(key: string) {
        return localStorage.getItem(key);
    }

    public static add(key: string, data: any) {
        localStorage.setItem(key, data);
    }

    public static update(key: string, data: any) {
        localStorage.setItem(key, data);
    }

    public static remove(key: string) {
        localStorage.removeItem(key);
    }

    public static reset() {
        localStorage.clear();
    }

}
