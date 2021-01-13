import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SurveyDetailsComponent } from './surveys/survey-details/survey-details.component';
import { SurveyListComponent } from './surveys/survey-list/survey-list.component';
import { LoginComponent } from "./user/login/login.component";
import { RegistrationComponent } from "./user/registration/registration.component";
import {AdminComponent} from "./user/dashboard/admin/admin.component";
import {SurveyToFillComponent} from "./surveys/survey-to-fill/survey-to-fill.component";
import {FillComponent} from "./surveys/fill/fill.component";
import {AuthGuard} from "./auth/auth.guard";
import {RoleGuard} from "./auth/role.guard";
import {Role} from "./models/role";

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
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [Role.Admin] },
    children: [
      {
        path: "",
        redirectTo: "surveys",
        pathMatch: "full",
      },
      {
        path: "surveys",
        component: SurveyListComponent,
      },
      {
        path: "registration",
        component: RegistrationComponent,
      },
      {
        path: "survey/:id",
        component: SurveyDetailsComponent,
      },
    ]
  },
  {
    path: "user",
    canActivate: [AuthGuard, RoleGuard],
    data: { roles: [Role.User] },
    children: [
      {
        path: "",
        redirectTo: "surveys",
        pathMatch: "full",
        data: { roles: [Role.User] }
      },
      {
        path: "surveys",
        component: SurveyToFillComponent,
        data: { roles: [Role.User] }
      },
      {
        path: "surveys/fill/:id",
        component: FillComponent,
        data: { roles: [Role.User] }
      }
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
