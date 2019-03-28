import { TestBed } from '@angular/core/testing';

import { CharacterApiService } from './character-api.service';

describe('CharacterApiService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: CharacterApiService = TestBed.get(CharacterApiService);
    expect(service).toBeTruthy();
  });
});
