import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from './Modals/Login';
import { Staff } from './Modals/Staff';
import { catchError, shareReplay, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class HttpService {


  constructor(private http: HttpClient) {

    //if (localStorage.getItem("TOKEN") == null) {
      //this.Login("Praveen Sankar", "new password");
    //}
  }

  Login(username : string, password : string) {
    this.http.post<Login>("https://localhost:5001/api/account/logon", {userName : username, password : password}).pipe(catchError(err=> {return throwError(err)})).subscribe(res=> {localStorage.setItem("TOKEN", res.token)});  
  }

  FetchAllStaff() {
    return this.http.get<Staff[]>("https://localhost:5001/api/staff").pipe(shareReplay());
  }
}
