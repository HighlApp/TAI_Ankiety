import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { ReactiveFormsModule, FormsModule } from "@angular/forms";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { AppRoutingModule } from "./app-routing.module";
import { UserService } from "./shared/user.service";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
// import { ToastrModule } from "ngx-toastr";
import { LoginComponent } from "./user/login/login.component";
import { AuthInterceptor } from "./auth/auth.interceptor";
import { MaterialModule } from "./material/material.module";
// import { NavbarComponent } from "./navbar/navbar.component";
// import { SidebarComponent } from "./sidebar/sidebar.component";
import { AppComponent } from "./app.component";
import { AuthService } from "./shared/auth.service";

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    ReactiveFormsModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule,
    FormsModule,
    // ToastrModule.forRoot({
    //   progressBar: true
    // })
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
