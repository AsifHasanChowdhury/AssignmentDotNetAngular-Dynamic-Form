import { TestBed } from '@angular/core/testing';

import { TransletLocalHostService } from './translet-local-host.service';

describe('TransletLocalHostService', () => {
  let service: TransletLocalHostService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(TransletLocalHostService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
