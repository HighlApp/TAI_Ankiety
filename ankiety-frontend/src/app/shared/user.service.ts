import { Injectable } from "@angular/core";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { HttpClient } from "@angular/common/http";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: "root"
})
export class UserService {
  private readonly BaseURI: string;
  private emailPattern = /^[a-z0-9._%+-]+@[a-z0-9.-]+\.[a-z]{2,4}$/

  constructor(private fb: FormBuilder, private http: HttpClient) {
    this.BaseURI = environment.apiUrl;
  }


  formModel = this.fb.group({
    UserName: ["", [Validators.required, Validators.minLength(5)]],
    Email: ["", [Validators.pattern(this.emailPattern), Validators.required]],
    Name: ["", Validators.required],
    Surname: ["", Validators.required],
    PhoneNumber: [""],
    Passwords: this.fb.group(
      {
        Password: ["", [Validators.required, Validators.minLength(8)]],
        ConfirmPassword: ["", Validators.required]
      },
      { validator: this.comparePasswords }
    ),
    UserType: ["", Validators.required]
  });

  comparePasswords(fb: FormGroup) {
    let confirmPswrdCtrl = fb.get("ConfirmPassword");

    if (
      confirmPswrdCtrl.errors == null ||
      "passwordMismatch" in confirmPswrdCtrl.errors
    ) {
      if (fb.get("Password").value != confirmPswrdCtrl.value)
        confirmPswrdCtrl.setErrors({ passwordMismatch: true });
      else confirmPswrdCtrl.setErrors(null);
    }
  }

  register() {
    var body = {
      UserName: this.formModel.value.UserName,
      Email: this.formModel.value.Email,
      Name: this.formModel.value.Name,
      Surname: this.formModel.value.Surname,
      Password: this.formModel.value.Passwords.Password,
      UserType: this.formModel.value.UserType,
      PhoneNumber: this.formModel.value.PhoneNumber
    };

    return this.http.post(this.BaseURI + "/ApplicationUser/Register", body);
  }

  login(formData: any) {
    const reqData = {
      email: formData.UserName,
      password: formData.Password
    }
    return this.http.post(this.BaseURI + "/identity/sign-in", reqData);
  }

  getUsers() {
    return this.http.get(this.BaseURI + "/UserProfile/Users");
  }
}
