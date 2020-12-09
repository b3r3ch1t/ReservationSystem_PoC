import { Pipe, PipeTransform } from '@angular/core';

import { orderBy,sortBy } from 'lodash';

@Pipe({name: 'sort'})


export class SortPipe implements PipeTransform{


  transform(items: any[], field: string, reverse: boolean = false): any[] {

     console.log("value=", items);
     console.log("field=", field);
     console.log("reverse=", reverse);

      if (!items) return [];

      if (field) items.sort((a, b) => a[field] > b[field] ? 1 : -1);
      else items.sort((a, b) => a > b ? 1 : -1);

      if (reverse) items.reverse();

      return items;
    }

}
