import { PipeTransform, Pipe } from "@angular/core";

@Pipe({
    name: 'contains',
})
export class ContainsPipe implements PipeTransform {
    transform(items: any[], term: any): any {

        if (items == null || term == null) return false;

        let key = Object.keys(term)[0];

        if (key == null)
            return false;

        return items.filter(item => item[key] == term[key]).length > 0;
    }
}