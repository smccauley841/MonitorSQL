/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { SqlinstanceService } from './sqlinstance.service';

describe('Service: Sqlinstance', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [SqlinstanceService]
    });
  });

  it('should ...', inject([SqlinstanceService], (service: SqlinstanceService) => {
    expect(service).toBeTruthy();
  }));
});
