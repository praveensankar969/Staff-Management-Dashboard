import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from './Modals/Login';
import { Staff } from './Modals/Staff';
import { shareReplay } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class HttpService {

  
  constructor(private http: HttpClient) { 
    if(localStorage.getItem("TOKEN")==null){
      this.Login({userName: "Praveen Sankar", password : "new password"});
    }
  }

  Login(login : Login){
    this.http.post("https://localhost:5001/api/account/logon", login).subscribe(res=> localStorage.setItem("TOKEN", res.toString()));
  }

  FetchAllStaff(){
    return this.http.get<Staff[]>("https://localhost:5001/api/staff").pipe(shareReplay());
  }
}
