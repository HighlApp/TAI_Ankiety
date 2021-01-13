import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { UserService } from 'src/app/shared/user.service';
import { MatDialogRef } from "@angular/material/dialog";
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-change-password',
  templateUrl: './change-password.component.html',
  styleUrls: ['./change-password.component.css']
})
export class ChangePasswordComponent implements OnInit {
  form: any;

  constructor(
      private fb: FormBuilder,
      private userService: UserService,
      public dialogRef: MatDialogRef<ChangePasswordComponent>,
      private toastr: ToastrService
  ) {
  }


  ngOnInit() {
    this.form = this.fb.group({
      Passwords: this.fb.group(
          {
            ActualPassword: ["", [Validators.required, Validators.minLength(8)]],
            NewPassword: ["", [Validators.required, Validators.minLength(8)]],
            ConfirmPassword: ["", Validators.required]
          },
          { validator: this.comparePasswords }
      )
    })
  }

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get("ConfirmPassword");

    if (
        confirmPswrdCtrl.errors == null ||
        "passwordMismatch" in confirmPswrdCtrl.errors
    ) {
      if (fb.get("NewPassword").value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else confirmPswrdCtrl.setErrors(null);
    }
  }

  onChangePassword() {
    var passwords = this.form.get("Passwords");

    var changePasswordModel = {
      password: passwords.controls.ActualPassword.value,
      newPassword: passwords.controls.NewPassword.value
    };

    this.userService.changePassword(changePasswordModel).subscribe((res) => {
          this.dialogRef.close();
          console.log('Pomyślnie zmieniono hasło.')
          this.toastr.success("Pomyślnie zmieniono hasło.", "Zmiana hasła");
        },
        (err) => {
          console.log('Wystąpił błąd podczas zmiany hasła. Spróbuj ponownie.')
          this.toastr.error("Wystąpił błąd podczas zmiany hasła. Spróbuj ponownie.", "Zmiana hasła");
        });
  }

  onCloseClick() {
    this.dialogRef.close();
  }

  onKeyDown(event: any) {
    return event.keyCode != 32;
  }
}
