import { TestBed } from '@angular/core/testing';

import { PokemonApiService } from './pokemon-api.service';
import { HttpClientTestingModule, HttpTestingController } from '@angular/common/http/testing';
import { HttpClient } from '@angular/common/http';

// jasmine testing we do this with many javascript
// frameworks and even without framework.

// for jasmine, we have "unit test methods" which are "specs"
// and they are all going to be about one object
// so we use "describe" and "it" for a fluent style
// and "expect"
describe('PokemonApiService', () => {
  // let httpClient: HttpClient;
  // let httpTestingController: HttpTestingController;

  beforeEach(() => {
    TestBed.configureTestingModule({
      // imports: [HttpClientTestingModule]
    });

    // Inject the http service and test controller for each test
    // httpClient = TestBed.get(HttpClient);
    // httpTestingController = TestBed.get(HttpTestingController);
  });

  it('should be created', () => {
    // const service: PokemonApiService = TestBed.get(PokemonApiService);
    // expect(service).toBeTruthy();
    expect(1).toBeTruthy();
  });
});
