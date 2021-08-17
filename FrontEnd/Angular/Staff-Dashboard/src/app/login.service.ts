import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  sub = new BehaviorSubject<boolean>(false);

  constructor() { }

  Login(){
    this.sub.next(true);
  }

  Logout(){
    this.sub.next(false);
  }

  Isloggedin(){
    return this.sub.getValue();
  }
}
