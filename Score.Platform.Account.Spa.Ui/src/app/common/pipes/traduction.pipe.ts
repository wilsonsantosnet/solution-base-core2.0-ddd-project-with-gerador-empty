import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'traduction',
})
export class TraductionPipe implements PipeTransform {
  transform(sourceArray: string, defaultValue: string) {

    if (!sourceArray)
      return defaultValue;

    if (!sourceArray[defaultValue])
      return defaultValue;

    return sourceArray[defaultValue].label;
  }
}
