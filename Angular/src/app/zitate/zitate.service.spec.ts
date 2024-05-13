import { TestBed } from '@angular/core/testing';

import { ZitateService } from './zitate.service';

describe('ZitateService', () => {
  let service: ZitateService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ZitateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
