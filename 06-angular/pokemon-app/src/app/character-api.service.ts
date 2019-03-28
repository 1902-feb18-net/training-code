import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Character } from './models/character';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CharacterApiService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Character[]> {
    let baseUrl = environment.charApiUrl;
    console.log(`Making request at API url ${baseUrl}`);
    let url = `${baseUrl}/api/character`;

    return this.http.get<Character[]>(url);
  }
}
