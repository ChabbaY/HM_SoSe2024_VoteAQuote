import { TestBed } from '@angular/core/testing';

import { AutorenService } from './autoren.service';

describe('AutorenService', () => {
  let service: AutorenService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AutorenService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
