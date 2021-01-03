import { FormControl, FormGroupDirective, NgForm } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(
    _control: FormControl | null,
    _form: FormGroupDirective | NgForm | null
  ): boolean {
    return false;
  }
}
