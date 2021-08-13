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

  async GetAllStaff() {
    await this.http.FetchAllStaff().pipe(map(res=> res.sort((a,b)=> a.id - b.id))).subscribe(res=> this.subject.next(res));
  }



}
