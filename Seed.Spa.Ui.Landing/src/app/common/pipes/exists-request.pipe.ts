import { Pipe, PipeTransform } from '@angular/core';
import { ApiService } from '../services/api.service';
import { retry } from 'rxjs/operators';
import { fail } from 'assert';

@Pipe({
  name: 'existsRequest',
})
export class ExistsRequestPipe implements PipeTransform {

  constructor(private api: ApiService<any>) {

  }

  transform(model: any, dataItem: string): Promise<boolean> {

    if (model) {
      return this.api.enableLoading(false).setResource(dataItem)
        .get({ id: model })
        .toPromise<boolean>()
        .then(((response: any) => {
          return response.data ? true : false;
        }));
    }
  }
}
