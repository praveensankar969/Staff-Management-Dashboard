import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
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
  staffs : Staff[] = [];
  toggleDropdown = false;
  selectedRows: number[] = [];
  typeSelected: string = "";
  filtered = false;
  idIcon = "south";
  nameIcon = "south";

  constructor(public staffService: StaffService, private router: Router) { 
    
  }

  ngOnInit(): void {
    this.Fetch();
  }

  Fetch() {
    this.staffService.GetAllStaff().pipe(tap(res => this.total = res.length)).subscribe(res=> this.staffs = res);
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

  SortId() {
    if (this.filtered) {
      if (this.idIcon == "south") {
        this.idIcon = "north";
        this.staffs.filter(x => x.type == this.typeSelected).
          sort((a, b) => b.id - a.id);
      }
      else {
        this.idIcon = "south";
        this.staffs.filter(x => x.type == this.typeSelected).
          sort((a, b) => a.id - b.id);
      }
    }
    else {
      if (this.idIcon == "south") {
        this.idIcon = "north";
        this.staffs.sort((a, b) => b.id - a.id);
      }
      else {
        this.idIcon = "south";
        this.staffs.sort((a, b) => a.id - b.id);
      }
    }

  }

  SortName() {
    if (this.filtered) {
      if (this.nameIcon == "south") {
        this.nameIcon = "north";
        this.staffs.filter(x => x.type == this.typeSelected).
          sort((a, b) => a.userName.localeCompare(b.userName));
      }
      else {
        this.nameIcon = "south";
        this.staffs.filter(x => x.type == this.typeSelected).
          sort((a, b) => b.userName.localeCompare(a.userName));
      }
    }
    else {
      if (this.nameIcon == "south") {
        this.nameIcon = "north";
        this.staffs.sort((a, b) => b.id - a.id);
      }
      else {
        this.nameIcon = "south";
        this.staffs.sort((a, b) => a.id - b.id);
      }
    }
  }

  DeletedSelected() {
    var choice = "";
    if (confirm("Are you sure you want to delete this staff?")) {
      choice = "ok";
    } else {
      choice = "cancel";
    }
    if (choice == "ok") {
      for (let index = 0; index < this.selectedRows.length; index++) {
        this.staffService.DeleteStaff(this.selectedRows[index]).subscribe(res=> {});
        this.staffs = this.staffs.filter(x=> x.id != this.selectedRows[index]);
      }
      this.staffService.GetAllStaff();
    }
  }

  DeleteStaff(id: number) {
    var choice = "";
    if (confirm("Are you sure you want to delete this staff?")) {
      choice = "ok";
    } else {
      choice = "cancel";
    }
    if (choice == "ok") {
      this.staffService.DeleteStaff(id).subscribe(res=> {
            this.selectedRows = this.selectedRows.filter(x => x != id);
            this.staffs = this.staffs.filter(x=> x.id != id)});
      this.staffService.GetAllStaff();
    }

  }

  TypeSelected(event: Event) {
    this.toggleDropdown = false;
    this.filtered = true;
    this.typeSelected = (event.target as HTMLAnchorElement).innerHTML;
    this.prevIndex = 0;
    this.nextIndex = this.perPage;
    this.Fetch();
    this.staffs = this.staffs.filter(x => x.type == this.typeSelected);
  }

  ClearFilter() {
    if(this.toggleDropdown){
      this.toggleDropdown = !this.toggleDropdown;
    }
    this.filtered = false;
    this.prevIndex = 0;
    this.nextIndex = this.perPage;
    this.Fetch();
  }

  EditInline(id: number) {
    this.router.navigate(["edit", id]);
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
