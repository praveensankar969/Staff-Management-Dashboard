import { Component, OnInit } from '@angular/core';
import { Form, NgForm, NgModel } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { Staff } from '../Modals/Staff';
import { StaffService } from '../staff.service';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-edit-details',
  templateUrl: './edit-details.component.html',
  styleUrls: ['./edit-details.component.css']
})
export class EditDetailsComponent implements OnInit {

  nameTxt = "Username is required";
  passTxt = "Password is required";
  dateTxt = "Date of joining is required";
  expTxt = "Experience is required";
  subTxt = "Subject is required";
  phnTxt = "Phone number is required";
  id: number = -1;
  staff: Staff | null = null;
  allStaffs: Staff[] = [];
  constructor(public staffService: StaffService, private route: ActivatedRoute, private router: Router) {
    this.route.params.subscribe(res => this.id = res['id']);
  }

  ngOnInit() {
    this.staffService.GetStaff(this.id).
      pipe(catchError(err=>{ this.router.navigate(["/notfound"]);
                              return throwError(err)}))
                                .subscribe(res => {this.staff = res;});
    this.staffService.GetAllStaff().subscribe(res => this.allStaffs = res);
  }

  EditData(form: NgForm) {
    if (!form.pristine) {
      this.staffService.UpdateStaff(this.staff!).subscribe(res=> console.log("Update success"));
    }
  }

  ValidateField(field: NgModel) {
    if (!field.pristine) {
      switch(field.name){
        case "name":{
          if(this.allStaffs.find(x=> x.userName == field.value) == undefined){
            if(!field.valid){
              this.nameTxt = "Username is required";
              return false;
            }
            return true;
          }
          else{
            this.nameTxt = "Username not unique";
            return false;
          }
        }
        case "password":{
          if(!field.valid){
            this.passTxt = "Password is required";
            return false;
          }
          if(this.staff!.password.length<4){
            this.passTxt = "Password should be minimum 4 characters";
            return false;
          }
          else{
            return true;
          }
        }
        case "date":{
          var date = new Date(this.staff!.dateOfJoining);
          var today = new Date();
          if(date > today){
            if(!field.valid){
              this.dateTxt = "Date of joining is required";
              return false;
            }
            this.dateTxt = "Date of Joining cannot be in future";
            return false;
          }
          else{
            return true;
          }
        }
        case "experience":{
          if(this.staff!.experience>30){
            if(!field.valid){
              this.expTxt = "Experience is required";
              return false;
            }
            this.expTxt = "Experience cannot be greater than 30";
            return false;
          }
          else{
            return true;
          }
        }
        case "phone":{
          if(!field.valid){
            this.phnTxt = "Phone number is required";
            return false;
          }
          else if((field.value + "").length !=10){
            this.phnTxt = "Phonenumber must be 10 digits";
            return false;
          }
          else{
            return true;
          }
        }
        default :{
          return false;
        }
      }
    }
    else {
      return true;
    }
  }

}


