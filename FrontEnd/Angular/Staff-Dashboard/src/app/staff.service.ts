import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpService } from './http.service';
import { Staff } from './Modals/Staff';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  
  private subject = new BehaviorSubject<Staff[]>([]);
  obs = this.subject.asObservable();

  constructor(private http: HttpService) {
  }

  GetAllStaff() {
    this.http.FetchAllStaff().pipe(map(res=> res.sort((a,b)=> a.id - b.id))).
      subscribe(res=> {this.subject.next(res); console.log("Fetch done")});
  }

  DeleteStaff(id : number){
    this.subject.next(this.subject.getValue().filter(x=> x.id != id));
    this.http.DeleteStaff(id).subscribe(res=> console.log("Delete Success"));
  }

  GetStaff(id: number) {
    return this.http.FetchStaff(id);
  }

  UpdateStaff(staff : Staff){
    this.http.UpdateStaffDetail(staff).subscribe(res=> console.log("Update Success"));
    var staffs = this.subject.getValue();
    var index = staffs.findIndex(x=> x.id == staff.id);
    staffs[index] = staff;
    console.log(staffs)
    this.subject.next(staffs);
    this.GetAllStaff();
  }

}