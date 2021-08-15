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
      subscribe(res=> {this.subject.next(res)});
  }

  DeleteStaff(id : number){
    this.subject.next(this.subject.getValue().filter(x=> x.id != id));
    this.http.DeleteStaff(id).subscribe(res=> console.log("Delete Success"));
  }

}