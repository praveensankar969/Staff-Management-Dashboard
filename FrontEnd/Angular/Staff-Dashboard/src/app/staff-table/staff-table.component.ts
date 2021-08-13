import { Component, OnInit } from '@angular/core';
import { Observable, Observer } from 'rxjs';
import { map, tap } from 'rxjs/operators';
import { Staff } from '../Modals/Staff';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-staff-table',
  templateUrl: './staff-table.component.html',
  styleUrls: ['./staff-table.component.css']
})
export class StaffTableComponent implements OnInit { 
  
  prevIndex= 0;
  nextIndex = 10;
  perPage = 10;
  total = 0;
  staffsObs = new Observable<Staff[]>();
  toggleDropdown = false;
  selectedRows : number[] = [];
  typeSelected : string = "";
  filtered = false;
  
  constructor(public staffService : StaffService) { }

  ngOnInit(): void {
    this.staffService.GetAllStaff();
    this.Fetch();
  }

  Fetch(){
    this.staffsObs = this.staffService.obs.pipe(tap(res=> this.total = res.length),map(res=> { 
      return res.slice(this.prevIndex, this.nextIndex);}));
  }

  CheckboxSelected(event : Event){
    var mode = (event.target as HTMLInputElement).checked;
    var id = (event.target as HTMLInputElement).getAttribute('id');
    if(mode){
      this.selectedRows.push(Number(id));
    }
    else{
      this.selectedRows = this.selectedRows.filter(x=> x !=Number(id));
    }
  }

  TypeSelected(event : Event){
    this.toggleDropdown = false;
    this.filtered = true;
    this.typeSelected = (event.target as HTMLAnchorElement).innerHTML;
  }

  PreviousPage(){
    this.prevIndex = this.prevIndex -this.perPage;
    this.nextIndex = this.nextIndex -this.perPage;
    this.Fetch();
  }

  NextPage(){
    console.log(this.total)
    this.prevIndex = this.prevIndex +this.perPage;
    this.nextIndex = this.nextIndex +this.perPage;
    this.Fetch();
  }


}
