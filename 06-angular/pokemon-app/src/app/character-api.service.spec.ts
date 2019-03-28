import { TestBed } from '@angular/core/testing';

import { CharacterApiService } from './character-api.service';
import { HttpClientTestingModule } from '@angular/common/http/testing';

describe('CharacterApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports: [ HttpClientTestingModule ]
  }));

  it('should be created', () => {
    const service: CharacterApiService = TestBed.get(CharacterApiService);
    expect(service).toBeTruthy();
  });
});
