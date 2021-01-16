import { Router } from "@angular/router";
import { ToastrService } from "ngx-toastr";
import { Role } from 'src/app/models/role';
import { Login } from "src/app/models/login";
import { Component, OnInit } from "@angular/core";
import { UserService } from "src/app/shared/user.service";

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  formModel: Login;

  constructor(
    private userService: UserService,
    private router: Router,
    private toastr: ToastrService) { }

  ngOnInit() {
    this.formModel = new Login();

    if (localStorage.getItem("token") != null) {
      if (JSON.parse(atob(localStorage.getItem("token").split(".")[1])).role == Role.Admin)
        this.router.navigateByUrl("/admin/surveys");
      else
        this.router.navigateByUrl("/user");
    }
  }

  onSubmit() {
    if (this.formModel.username != null && this.formModel.password != null) {
      this.userService.login(this.formModel).subscribe(
        (res: any) => {
          if (res.isError)
          {
            this.toastr.warning(
              "Niepoprawna nazwa użytkownika lub hasło. Spróbuj ponownie.",
              "Błąd logowania"
            );

            this.formModel = new Login();
          }
          else {
            localStorage.setItem("token", res.data.token);
            localStorage.setItem("user", this.formModel.username);

            if (res.data.role == "Administrator") {
              this.router.navigateByUrl("/admin");
            } else {
              this.router.navigateByUrl("/user");
            }
          }
        }
      );
    }
  }
}
