import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { Account } from './models/account';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      // check if user is logged in with help of session storage
      let account = <Account>sessionStorage['account'];
      if (account) {
        return true;
      }
      return false;
      // another way would be... here send request to /api/account/details
      // and check. instead of having done it earlier from login directly
  }

}
