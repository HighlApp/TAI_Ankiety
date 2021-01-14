import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from "./app-routing.module";
import { UserService } from "./shared/user.service";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { ToastrModule } from "ngx-toastr";
import { LoginComponent } from "./user/login/login.component";
import { AuthInterceptor } from "./auth/auth.interceptor";
import { MaterialModule } from "./material/material.module";
import { AppComponent } from "./app.component";
import { AuthService } from "./shared/auth.service";
import { SurveyListComponent } from './surveys/survey-list/survey-list.component';
import { SurveyComponent } from './surveys/survey/survey.component';
import { NewSurveyComponent } from './surveys/new-survey/new-survey.component';
import { MatRadioModule } from "@angular/material/radio";
import { SurveyDetailsComponent } from './surveys/survey-details/survey-details.component';
import { ConfirmationDialogComponent } from './shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { NewQuestionComponent } from './questions/new-question/new-question.component';
import { EditSurveyComponent } from './surveys/edit-survey/edit-survey/edit-survey.component';
import { EditTextComponent } from './questions/edit-text/edit-text.component';
import { EditChoiceComponent } from './questions/edit-choice/edit-choice.component';
import { AddChoiceComponent } from './questions/add-choice/add-choice.component';
import { AddTextComponent } from './questions/add-text/add-text.component';
import { RegistrationComponent } from './user/registration/registration.component';
import { AdminComponent } from './user/dashboard/admin/admin.component';
import { SidebarComponent } from './shared/sidebar/sidebar.component';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { ChangePasswordComponent } from './user/change-password/change-password.component';
import { SurveyToFillComponent } from './surveys/survey-to-fill/survey-to-fill.component';
import { InvitationComponent } from './surveys/survey-to-fill/invitation/invitation.component';
import { FillComponent } from './surveys/fill/fill.component';
import { SendInvitationsComponent } from './surveys/survey-details/send-invitations/send-invitations.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    SurveyListComponent,
    SurveyComponent,
    NewSurveyComponent,
    SurveyDetailsComponent,
    ConfirmationDialogComponent,
    NewQuestionComponent,
    EditSurveyComponent,
    EditTextComponent,
    EditChoiceComponent,
    AddChoiceComponent,
    AddTextComponent,
    RegistrationComponent,
    AdminComponent,
    SidebarComponent,
    NavbarComponent,
    ChangePasswordComponent,
    SurveyToFillComponent,
    InvitationComponent,
    FillComponent,
    SendInvitationsComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    FormsModule,
    MatRadioModule,
    ToastrModule.forRoot({
      progressBar: true
    })
  ],
  providers: [
    AuthService,
    UserService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
  ],
  bootstrap: [AppComponent],
  entryComponents: [
  ]
})
export class AppModule { }
