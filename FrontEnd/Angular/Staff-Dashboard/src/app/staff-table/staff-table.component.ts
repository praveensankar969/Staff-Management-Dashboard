import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Staff } from '../Modals/Staff';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-staff-table',
  templateUrl: './staff-table.component.html',
  styleUrls: ['./staff-table.component.css']
})
export class StaffTableComponent implements OnInit { 
  
  constructor(public staffService : StaffService) { }

  ngOnInit(): void {
    this.staffService.GetAllStaff();
  }

  


}
