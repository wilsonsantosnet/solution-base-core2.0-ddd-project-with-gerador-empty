
import { map } from 'rxjs/operators';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(public authService: AuthService, private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): Observable<boolean> | boolean {
    this.authService.getCurrentUser((result: any, firstTime: any) => {
      var permissions = JSON.parse(result.claims.tools);
      var canAccess = permissions.filter((item) => {

        if (item.Route + "/create" == state.url && item.CanWrite)
          return true;

        if (state.url.startsWith(item.Route + "/edit/") && item.CanWrite)
          return true;

        if (state.url.startsWith(item.Route + "/details/") && state.url && item.CanRead)
          return true;

        if (state.url.startsWith(item.Route + "/print/") && state.url && item.CanRead)
          return true;


        return item.Route == state.url;

      }).length > 0;
      if (!canAccess) {
        this.router.navigate(["/unauthorized"]);
        return false;
      }
    });
    return this.authService.IsAuthApiVerify().pipe(map((response) => {
      if (response.status == 401 || response.status == 403) {
        this.router.navigate(["/login"]);
        return false;
      }
      return true;
    }));
  }

}
