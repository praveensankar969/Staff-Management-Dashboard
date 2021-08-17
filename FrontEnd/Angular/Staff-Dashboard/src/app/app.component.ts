import { Component } from '@angular/core';
import { StaffService } from './staff.service';
import { LoginService } from './login.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Staff-Dashboard';
  constructor(public loginService: LoginService) {

    if (localStorage.getItem("TOKEN") == null) {
      this.loginService.Logout();
    }
    else {
      this.loginService.Login();
    }

  }

  Logout() {
    localStorage.removeItem("TOKEN");
    this.loginService.Logout();
  }


}
