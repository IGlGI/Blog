import {Pipe, PipeTransform} from '@angular/core';

@Pipe({
  name: 'searchItem'
})
export class SearchPipe implements PipeTransform {

  transform(items: any[], search: string, prop: string = 'name'): any {
    if (!search.trim()){
      return items;
    }

    return items?.filter((item) => {
      return item[prop].toLowerCase().includes(search.toLowerCase());
    });
  }
}
