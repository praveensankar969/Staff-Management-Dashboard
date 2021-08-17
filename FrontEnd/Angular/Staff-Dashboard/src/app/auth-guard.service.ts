import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthGuardService implements CanActivate {
  constructor(private router : Router) {}

  canActivate(route: ActivatedRouteSnapshot,state: RouterStateSnapshot): Observable<boolean>|Promise<boolean>|boolean {
  
    var user = localStorage.getItem("TOKEN");
    if(user!=null){
      return true;
    }
    else{
      this.router.navigate(["/login"]);
      return false;
    }


  }
}
