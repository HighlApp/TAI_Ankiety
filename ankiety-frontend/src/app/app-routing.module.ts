import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyDetailsComponent } from './surveys/survey-details/survey-details.component';
import { SurveyListComponent } from './surveys/survey-list/survey-list.component';
import { LoginComponent } from "./user/login/login.component";

const routes: Routes = [
  {
    path: "",
    redirectTo: "login",
    pathMatch: "full"
  },
  {
    path: "login",
    component: LoginComponent
  },
  {
    path: "admin/survey/:id",
    component: SurveyDetailsComponent
  },
  {
    path: "admin/surveys",
    component: SurveyListComponent
  },
  {
    path: '**',
    redirectTo: "login"
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
