import { TestBed } from '@angular/core/testing';
import { HttpClientTestingModule } from '@angular/common/http/testing';
import { RankingService } from './ranking.service';

describe('RankingService', () => {
  let service: RankingService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      imports: [HttpClientTestingModule]
    });
    service = TestBed.inject(RankingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
