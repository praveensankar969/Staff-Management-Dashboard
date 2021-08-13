import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { StaffTableComponent } from './staff-table/staff-table.component';
import {HttpClientModule, HTTP_INTERCEPTORS} from '@angular/common/http';
import { HttpInterceptService } from './http-intercept.service';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    StaffTableComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [{
    provide :  HTTP_INTERCEPTORS,
    useClass : HttpInterceptService,
    multi : true
  }],
  bootstrap: [AppComponent]
})
export class AppModule { }
