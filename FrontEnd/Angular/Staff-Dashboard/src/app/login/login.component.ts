import { Component, OnInit } from '@angular/core';
import { HttpService } from '../http.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { Router } from '@angular/router';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  username = "";
  password = "";
  error = false;
  constructor(private http : HttpService, private router : Router, private loginSer: LoginService) { }

  ngOnInit(): void {
  }

  Login(){
    this.http.Login(this.username, this.password).
      pipe(catchError(err=> {this.error = true; return throwError(err)})).
        subscribe(res=> {localStorage.setItem("TOKEN", res.token);
                          this.router.navigate(["/"]); this.loginSer.Login()});

  }
}
