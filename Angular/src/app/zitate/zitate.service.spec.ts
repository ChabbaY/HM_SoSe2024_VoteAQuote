import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { ZitateService } from './zitate.service';

describe('ZitateService', () => {
  let service: ZitateService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(ZitateService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
