import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { StaffTableComponent } from './staff-table/staff-table.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { HttpInterceptService } from './http-intercept.service';
import { EditDetailsComponent } from './edit-details/edit-details.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    StaffTableComponent,
    EditDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    CommonModule
  ],
  providers: [{
    provide :  HTTP_INTERCEPTORS,
    useClass : HttpInterceptService,
    multi : true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
