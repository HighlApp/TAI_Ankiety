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
    Email: ["", [Validators.pattern(this.emailPattern), Validators.required]],
    Passwords: this.fb.group(
      {
        Password: ["", [Validators.required, Validators.minLength(8)]],
        ConfirmPassword: ["", Validators.required]
      },
      { validator: this.comparePasswords }
    ),
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
      Email: this.formModel.value.Email,
      Password: this.formModel.value.Passwords.Password,
    };

    return this.http.post(this.BaseURI + "/identity/sign-up", body);
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
