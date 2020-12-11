import { Injectable } from '@angular/core';
import { ValidatorFn, AbstractControl } from '@angular/forms';


@Injectable({
  providedIn: 'root'
})


export class CustomValidatorsService {

  constructor() { }


  validateContactName(contactName: AbstractControl) {

    return new Promise(resolve => {
      setTimeout(() => {
        if (contactName.value != null ) {
          resolve({ userNameNotAvailable: true });
        } else {
          resolve(null);
        }
      }, 1000);
    });
  }

  validateContactType(contactType: string) {

    return contactType != null;
  }

  validatePhone(phone: string) {

    var regexp = new RegExp('^((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}$');

    var result = regexp.test(phone);

    return result;
  }

}
