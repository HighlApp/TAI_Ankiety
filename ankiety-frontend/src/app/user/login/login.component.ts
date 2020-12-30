import { Component, OnInit } from "@angular/core";
import { UserService } from "src/app/shared/user.service";
import { Router } from "@angular/router";
import { NgForm } from "@angular/forms";
// import { ToastrService } from "ngx-toastr";
import { AuthService } from "src/app/shared/auth.service";
import { Role } from 'src/app/models/role';
import { UserDetails } from 'src/app/models/userDetails';

@Component({
  selector: "app-login",
  templateUrl: "./login.component.html",
  styles: []
})
export class LoginComponent implements OnInit {
  formModel = {
    UserName: "",
    Password: ""
  };

  constructor(
    private userService: UserService,
    private router: Router,
    // private toastr: ToastrService,
    private authService: AuthService
  ) { }

  ngOnInit() {
    if (localStorage.getItem("token") != null) {
      if (JSON.parse(atob(localStorage.getItem("token").split(".")[1])).role == Role.Admin)
        this.router.navigateByUrl("/admin");
      else
        this.router.navigateByUrl("/user");
    }
  }

  onSubmit(form: NgForm) {
    this.userService.login(form.value).subscribe(
      (res: any) => {
        localStorage.setItem("token", res.data.token);

        if (res.data.role == "Administrator") {
          this.router.navigateByUrl("/admin");
        } else {
          this.router.navigateByUrl("/user");
        }
      },
      err => {
        if (err.status == 400)
          console.log(err);
        //TODO wyświetlić komunikat o błędzie toastrem albo czymś innym
          // this.toastr.error(
          //   "Niepoprawna nazwa użytkownika lub hasło. Spróbuj ponownie.",
          //   "Logowanie"
          // );

        this.formModel.Password = "";
      }
    );
  }

}
