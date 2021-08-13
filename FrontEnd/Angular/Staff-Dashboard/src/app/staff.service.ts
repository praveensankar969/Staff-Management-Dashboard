import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { HttpService } from './http.service';
import { Staff } from './Modals/Staff';

@Injectable({
  providedIn: 'root'
})
export class StaffService {

  private subject = new BehaviorSubject<Staff[]>([]);
  obs = this.subject.asObservable();

  constructor(private http: HttpService) { }

  GetAllStaff(){
    this.http.FetchAllStaff().subscribe(res=> this.subject.next(res));
  }
  

}
