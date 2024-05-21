import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { AutorenService } from './autoren.service';

describe('AutorenService', () => {
  let service: AutorenService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(AutorenService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
