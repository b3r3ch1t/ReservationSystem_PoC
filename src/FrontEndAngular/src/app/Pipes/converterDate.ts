import { Pipe, PipeTransform } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';
import { DatePipe } from '@angular/common';



@Pipe({ name: 'convertDate',pure: false })


export class ConverterDatePipe  implements PipeTransform{


  constructor(
    public translateService: TranslateService
    ) {

      console.log(this.translateService.currentLang)
    }


    transform(value: string, args: string) : string {

      let currentLang = this.translateService.currentLang;

      const ngPipe = new DatePipe(currentLang);
      return ngPipe.transform(value, 'EEEE, MMM, dd \'at\' h:mm a');

    }

}
