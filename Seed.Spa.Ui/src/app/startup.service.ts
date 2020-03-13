import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { GlobalService } from './global.service';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable()
export class StartupService {


  constructor(private http: Http) { }

  load(): Promise<any> {

    return new Promise((resolve: any) => {

      let v = Math.random();
      let jsonFileURL: string = "assets/appsettings." + environment.name + ".json?v=" + v;
      this.http.get(jsonFileURL)
        .toPromise()
        .then((data: any) => {
          data = data.json();
          return resolve(GlobalService.setAppsettings(data));
        })
        .catch((err: any) => {
          console.log("StartupService", v, err);
          Promise.reject(err);
        });
    });
  }



}
