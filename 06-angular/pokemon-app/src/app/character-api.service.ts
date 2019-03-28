import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Character } from './models/character';
import { Observable } from 'rxjs';
import { Login } from './models/login';
import { Account } from './models/account';

@Injectable({
  providedIn: 'root'
})
export class CharacterApiService {

  constructor(private http: HttpClient) { }

  getAll(): Observable<Character[]> {
    const baseUrl = environment.charApiUrl;
    console.log(`Making request at API url ${baseUrl}`);
    const url = `${baseUrl}/api/character`;

    return this.http.get<Character[]>(url, { withCredentials: true });
  }

  login(login: Login): Observable<Account> {
    // first, send request to login
    const url = `${environment.charApiUrl}/api/account/login`;
    return this.http.post(url, login, { withCredentials: true }).pipe(res => {
      // then, send request to details
      const url2 = `${environment.charApiUrl}/api/account/details`;
      return this.http.get<Account>(url2, { withCredentials: true }).pipe(account => {
        // when we get that, save in session storage the logged in user's info
        // (so if client refreshes page, we still have it)
        sessionStorage.setItem('account', JSON.stringify(account));
        // return the account details to the one calling this method
        return account;
      });
    });
  }
}
