import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { UserService } from 'src/app/shared/user.service';

@Component({
  selector: 'app-edit-user',
  templateUrl: './edit-user.component.html',
  styleUrls: ['./edit-user.component.css']
})
export class EditUserComponent implements OnInit {

  email: string;
  name: string;
  surname: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    private toastr: ToastrService,
    public dialogRef: MatDialogRef<EditUserComponent>,
    private userService: UserService) { }

  ngOnInit(): void {
    this.email = this.data.user.email;
    this.name = this.data.user.name;
    this.surname = this.data.user.surname;
  }

  onSubmit() {
    var request = {
      id: this.data.user.id,
      name: this.name,
      surname: this.surname,
      email: this.email
    };
    this.userService.updateUser(request).subscribe((res) => {
      this.updateData(request);
      this.dialogRef.close();
      this.toastr.success("Pomyślnie zaktualizowano dane użytkownika.", "Pomyślnie zaktualizowano");
    },
      (err) => {
        this.toastr.error("Nie można zaktualizować danych", "Wystąpił błąd");
      });
  }

  onCloseClick() {
    this.dialogRef.close();
  }

  textChanged() {
     return this.name != this.data.user.name 
     || this.surname != this.data.user.surname 
     || this.email != this.data.user.email;
  }

  isSomeFieldEmpty(){
    return this.name == "" || this.surname == "" || this.email == "";
  }

  updateData(request: any) {
    this.data.user.name = request.name;
    this.data.user.surname = request.surname;
    this.data.user.email = request.email;
  }
}
