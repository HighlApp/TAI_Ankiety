import { Component, OnInit } from "@angular/core";
import { UserService } from "src/app/shared/user.service";

@Component({
  selector: 'app-registration',
  templateUrl: './registration.component.html',
  styleUrls: ['./registration.component.css']
})
export class RegistrationComponent implements OnInit {
  private usernamePattern = /[A-Za-z0-9ąĄćĆęĘłŁńŃóÓśŚżŻźŹ]/

  //FIXME zastąpić albo naprawić toastr
  constructor(public service: UserService /*, private toastr: ToastrService */) { }

  ngOnInit() {
    this.service.formModel.reset();
  }

  onSubmit() {
    this.service.register().subscribe(
        (res: any) => {
          console.log(res);
          if (res.data.isSuccess) {
            this.service.formModel.reset();
            //FIXME zastąpić albo naprawić toastr
            console.log("Zarejestrowano użytkownika pomyślnie.");
            // this.toastr.success(
            //   "Zarejestrowano użytkownika pomyślnie.",
            //   "Rejestracja użytkownika"
            // );
          } else {
            console.log(res.errorMessages);
          }
        },
        err => {
          //FIXME zastąpić albo naprawić toastr
          console.log("Wystąpił błąd. Spróbuj ponownie.");
          // this.toastr.error("Wystąpił błąd. Spróbuj ponownie.", "Wystąpił błąd.");
        }
    );
  }

  onKeyClick(event: any) {
    return (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || (event.keyCode >= 97 && event.keyCode <= 105) || event.keyCode == 37 || event.keyCode == 39;
  }

  onKeyDown(event: any) {
    return event.key.match(this.usernamePattern) != null;
  }

  onDataInput(event: any) {
    return event.keyCode != 32;
  }
}
