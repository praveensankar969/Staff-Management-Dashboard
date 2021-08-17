import { Injectable } from '@angular/core';
import { BehaviorSubject, throwError } from 'rxjs';
import { catchError, first } from 'rxjs/operators';
import { Login } from './Modals/Login';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from './AppSettings';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  sub = new BehaviorSubject<boolean>(false);

  constructor(private http: HttpClient) { }

  Login(username : string, password : string) {
    return this.http.post<Login>(AppSettings.API_ENDPOINT+"account/logon", {userName : username, password : password}).
      pipe(catchError(err=> {return throwError(err)}), first());
  }

  Logout(){
    this.sub.next(false);
  }

  Isloggedin(){
    return this.sub.getValue();
  }
}
