import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Login } from './Modals/Login';
import { Staff } from './Modals/Staff';
import { catchError, shareReplay, tap } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { StaffAdd } from './Modals/StaffAdd';

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
    this.http.post<Login>("https://localhost:5001/api/account/logon", {userName : username, password : password}).
      pipe(catchError(err=> {return throwError(err)})).
        subscribe(res=> {localStorage.setItem("TOKEN", res.token)});  
  }

  FetchAllStaff() {
    return this.http.get<Staff[]>("https://localhost:5001/api/staff").pipe(catchError(err=> {return throwError(err)}));
  }

  DeleteStaff(id: number){
    return this.http.delete("https://localhost:5001/api/staff/"+id).pipe(catchError(err=> {return throwError(err)}));
  }

  FetchStaff(id: number){
    return this.http.get<Staff>("https://localhost:5001/api/staff/"+id).pipe(catchError(err=> {return throwError(err)}));
  }

  UpdateStaffDetail(staff : Staff){
    return this.http.put("https://localhost:5001/api/staff/"+staff.id, staff).pipe(catchError(err=> {return throwError(err)}));
  }

  AddStaffDetail(staff : StaffAdd){
    return this.http.post("https://localhost:5001/api/staff/addstaff", staff).pipe(catchError(err=> {return throwError(err)}));
  }
}
