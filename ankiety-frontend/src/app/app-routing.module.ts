import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyDetailsComponent } from './surveys/survey-details/survey-details.component';
import { SurveyListComponent } from './surveys/survey-list/survey-list.component';
import { LoginComponent } from "./user/login/login.component";
import { RegistrationComponent } from "./user/registration/registration.component";
import {AdminComponent} from "./user/dashboard/admin/admin.component";

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
    path: "admin",
    component: AdminComponent,
    // canActivate: [AuthGuard, RoleGuard],
    children: [
      {
        path: "",
        redirectTo: "surveys",
        pathMatch: "full",
        // data: { roles: [Role.Admin] }
      },
      {
        path: "surveys",
        component: SurveyListComponent,
        // data: { roles: [Role.Admin] }
      },
      {
        path: "registration",
        component: RegistrationComponent,
        // data: { roles: [Role.Admin] }
      },
      {
        path: "survey/:id",
        component: SurveyDetailsComponent,
        // data: { roles: [Role.Admin] }
      },
    ]
  },
  // {
  //   path: "admin/survey/:id",
  //   component: SurveyDetailsComponent
  // },
  // {
  //   path: "admin/surveys",
  //   component: SurveyListComponent
  // },
  // {
  //   path: "registration",
  //   component: RegistrationComponent,
  // },
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
