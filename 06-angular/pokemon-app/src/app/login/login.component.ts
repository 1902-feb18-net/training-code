import { Component, OnInit } from '@angular/core';
import { Login } from '../models/login';
import { CharacterApiService } from '../character-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login = new Login();

  constructor(
    private api: CharacterApiService,
    private router: Router
  ) { }

  ngOnInit() {
  }

  onSubmit() {
    this.login.rememberMe = true;
    this.api.login(this.login).then(() => {
      this.router.navigate(['/char']);
    },
    error => {
      // should inspect error and put useful info on page
      console.log(error);
    });
  }

}
