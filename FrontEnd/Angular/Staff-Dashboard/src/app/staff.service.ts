import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, throwError } from 'rxjs';
import { catchError, map, shareReplay } from 'rxjs/operators';
import { Staff } from './Modals/Staff';
import { StaffAdd } from './Modals/StaffAdd';
import { HttpClient } from '@angular/common/http';
import { AppSettings } from './AppSettings';

@Injectable({
  providedIn: 'root'
})
export class StaffService {
 
  private ENDPOINT : string = "staff";

  constructor(private http: HttpClient) {
  }

  GetAllStaff() {
    return this.http.get<Staff[]>(AppSettings.API_ENDPOINT+this.ENDPOINT).pipe(catchError(err=> {return throwError(err)}), 
        map(res=> res.sort((a,b)=> a.id - b.id)), shareReplay());
  }

  DeleteStaff(id : number){
    return this.http.delete(AppSettings.API_ENDPOINT+this.ENDPOINT+"/"+id).
      pipe(catchError(err=> {return throwError(err)}));
  }

  GetStaff(id: number) {
    return this.http.get<Staff>(AppSettings.API_ENDPOINT+this.ENDPOINT+"/"+id).
      pipe(catchError(err=> {return throwError(err)}));
  }

  UpdateStaff(staff : Staff){
    return this.http.put(AppSettings.API_ENDPOINT+this.ENDPOINT+"/"+staff.id, staff)
      .pipe(catchError(err=> {return throwError(err)}));
  }

  AddStaff(staff : StaffAdd){
    return this.http.post(AppSettings.API_ENDPOINT+this.ENDPOINT+"/addstaff", staff).pipe(catchError(err=> {return throwError(err)}))
  }

}