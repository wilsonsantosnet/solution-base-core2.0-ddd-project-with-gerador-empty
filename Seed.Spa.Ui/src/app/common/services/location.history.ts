import { Injectable } from '@angular/core';
import { CacheService } from './cache.service';
import { ECacheType } from '../../common/type-cache.enum';


@Injectable()
export class LocationHistoryService {

  public static saveLocal(resourceName: string, url: string) {
    CacheService.add("lastNavigation" + resourceName, url, ECacheType.LOCAL);
  }

  public static getLastNavigation(resourceName: string) {
    return CacheService.get("lastNavigation" + resourceName, ECacheType.LOCAL)
  }
}
