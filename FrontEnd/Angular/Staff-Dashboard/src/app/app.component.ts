import { Component } from '@angular/core';
import { StaffService } from './staff.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Staff-Dashboard';

  constructor(private staffService : StaffService) {
    this.staffService.GetAllStaff();
  }
}
