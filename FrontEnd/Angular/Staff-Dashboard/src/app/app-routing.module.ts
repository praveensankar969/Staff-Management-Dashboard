import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { EditDetailsComponent } from './edit-details/edit-details.component';
import { AuthGuardService } from './auth-guard.service';
import { AddDetailsComponent } from './add-details/add-details.component';

const routes: Routes = [
  {path: "", component: HomeComponent},
  {path: "edit/:id", component:EditDetailsComponent, canActivate:[AuthGuardService]},
  {path : "add-staff", component: AddDetailsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
