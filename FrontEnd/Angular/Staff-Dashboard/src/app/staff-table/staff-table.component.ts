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

  perPage = 10;
  prevIndex = 0;
  nextIndex = 10;
  total = 0;
  staffsObs = new Observable<Staff[]>();
  toggleDropdown = false;
  selectedRows: number[] = [];
  typeSelected: string = "";
  filtered = false;

  constructor(public staffService: StaffService) { }

  ngOnInit(): void {
    this.staffService.GetAllStaff();
    this.Fetch();
  }

  Fetch() {
    this.staffsObs = this.staffService.obs.pipe(tap(res => this.total = res.length));
  }

  CheckboxSelected(event: Event) {
    var mode = (event.target as HTMLInputElement).checked;
    var id = (event.target as HTMLInputElement).getAttribute('id');
    if (mode) {
      this.selectedRows.push(Number(id));
    }
    else {
      this.selectedRows = this.selectedRows.filter(x => x != Number(id));
    }
  }

  DeletedSelected() {
    for (let index = 0; index < this.selectedRows.length; index++) {
      this.selectedRows = this.selectedRows.filter(x=> x != this.selectedRows[index]);
      this.staffService.DeleteStaff(this.selectedRows[index]);
    }
    this.Fetch();
  }

  DeleteStaff(id: number) {
    this.selectedRows = this.selectedRows.filter(x=> x != id);
    this.staffService.DeleteStaff(id);
    this.Fetch();
  }

  TypeSelected(event: Event) {
    this.toggleDropdown = false;
    this.filtered = true;
    this.typeSelected = (event.target as HTMLAnchorElement).innerHTML;
    this.prevIndex = 0;
    this.nextIndex = this.perPage;
    this.staffsObs = this.staffService.obs.pipe(map(res =>
      res.filter(x => x.type == this.typeSelected)));
  }

  ClearFilter() {
    this.filtered = false;
    this.prevIndex = 0;
    this.nextIndex = this.perPage;
    this.Fetch();
  }

  EditInline(){
    console.log("Edit page");
  }


  PreviousPage() {
    this.prevIndex = this.prevIndex - this.perPage;
    this.nextIndex = this.nextIndex - this.perPage;
  }

  NextPage() {
    this.prevIndex = this.prevIndex + this.perPage;
    this.nextIndex = this.nextIndex + this.perPage;
  }


}
