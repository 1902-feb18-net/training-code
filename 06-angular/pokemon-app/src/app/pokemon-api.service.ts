import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Pokemon } from './models/pokemon';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { PokemonCollection } from './models/pokemon-collection';

// services are meant to be injected into whatever depends on them (DI)
// we use @Injectable decorator to define in what scope this services is reachable

@Injectable({
  providedIn: 'root'
})
export class PokemonApiService {
  baseUrl = 'https://pokeapi.co/api/v2/pokemon';

  constructor(private httpClient: HttpClient) { }

  // this class can be repository pattern for my components
  getByName(searchText: string): Observable<Pokemon> {
    const url = `${this.baseUrl}/${searchText}`;
    console.log(`sending request to ${url}`);
    const response = this.httpClient.get<Pokemon>(url);

    // rxJS gives us a type called Observable with
    // a lot of powerful functional / asynchronous behavior
    return response.pipe(catchError(error => {
      console.log('error:');
      console.log(error);
      // could inspect the error for what sort it is
      // (4xx status code, 5xx status code, httpclient failure itself)
      return throwError('Encountered an error communicating with the server.');
    }));
  }

  getAll(): Observable<PokemonCollection> {
    const url = `${this.baseUrl}`;
    console.log(`sending request to ${url}`);
    const response = this.httpClient.get<PokemonCollection>(url);
    return response.pipe(catchError(error => {
      console.log('error:');
      console.log(error);
      // could inspect the error for what sort it is
      // (4xx status code, 5xx status code, httpclient failure itself)
      return throwError('Encountered an error communicating with the server.');
    }));
  }
}
