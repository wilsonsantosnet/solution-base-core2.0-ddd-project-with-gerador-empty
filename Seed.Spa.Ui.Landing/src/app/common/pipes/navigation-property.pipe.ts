import { Pipe, PipeTransform } from '@angular/core';
import { conformToMask } from 'angular2-text-mask';

@Pipe({
  name: 'navigationProperty',
})
export class NavigationPropertyPipe implements PipeTransform {
  transform(object: any, property: string) {
    let tempObject: any = Object.assign(object || {});

    if (property && property.includes('.')) {
      for (let i = 0; i < property.split('.').length; i++) {
        if (tempObject) {
          tempObject = tempObject[property.split('.')[i]];
        }
      }

      return tempObject;
    }

    if (object[property])
      return object[property];
    else
      return eval("object." + property.toLowerCase());
  }
}
