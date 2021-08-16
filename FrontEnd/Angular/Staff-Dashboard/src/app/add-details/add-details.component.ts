import { Component, OnInit } from '@angular/core';
import { NgForm, NgModel } from '@angular/forms';
import { Staff } from '../Modals/Staff';
import { StaffAdd } from '../Modals/StaffAdd';
import { StaffService } from '../staff.service';

@Component({
  selector: 'app-add-details',
  templateUrl: './add-details.component.html',
  styleUrls: ['./add-details.component.css']
})
export class AddDetailsComponent implements OnInit {

  nameTxt = "Username is required";
  passTxt = "Password is required";
  dateTxt = "Date of joining is required";
  expTxt = "Experience is required";
  subTxt = "Subject is required";
  phnTxt = "Phone number is required";
  userName: string = "";
  password = "";
  experience = 0;
  dateOfJoining = "";
  subject = "";
  phone= "";
  type = "Teacher";


  allStaffs: Staff[] = [];
  constructor(public staffService: StaffService) { }

  ngOnInit(): void {
    this.staffService.obs.subscribe(res => this.allStaffs = res);
  }

  AddData(){
    var staff : StaffAdd = {
      userName : this.userName,
      password : this.password,
      experience : this.experience,
      dateOfJoining : new Date(this.dateOfJoining),
      subject : this.subject,
      phoneNumber : this.phone.toString(),
      type : this.type
    };
    this.staffService.AddStaff(staff);
  }

  TypeUpdate(event : Event){
    this.type = (event.target as HTMLSelectElement).value;
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
          if(field.value.length<4){
            this.passTxt = "Password should be minimum 4 characters";
            return false;
          }
          else{
            return true;
          }
        }
        case "date":{
          var today = new Date();
          if(field.value > today){
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
          if(field.value>30){
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
