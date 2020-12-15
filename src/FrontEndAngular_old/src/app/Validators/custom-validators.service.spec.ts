/* tslint:disable:no-unused-variable */

import { TestBed, inject, waitForAsync } from '@angular/core/testing';
import { CustomValidatorsService } from './custom-validators.service';

describe('Service: CustomValidators', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [CustomValidatorsService]
    });
  });

  it('should ...', inject([CustomValidatorsService], (service: CustomValidatorsService) => {
    expect(service).toBeTruthy();
  }));
});
