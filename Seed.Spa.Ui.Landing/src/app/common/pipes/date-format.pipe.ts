import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';

@Pipe({
    name: 'DateFormatPipe',
})
export class DateFormatPipe implements PipeTransform {
    transform(value: string) {
        var datePipe = new DatePipe("pt-BR");
        value = datePipe.transform(value, 'dd/MM/yyyy HH:mm');
        return value;
    }
}