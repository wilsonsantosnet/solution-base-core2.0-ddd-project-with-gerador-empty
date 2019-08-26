import { Pipe, PipeTransform } from '@angular/core';
import { conformToMask } from 'angular2-text-mask';

@Pipe({
    name: 'maskFormatPipe',
})
export class MaskFormatPipe implements PipeTransform {
    transform(value: string, masked: any) {
        var transformed = conformToMask(
            value,            
            masked.mask,
            { guide: false }
        )        

        return transformed.conformedValue;
    }
}