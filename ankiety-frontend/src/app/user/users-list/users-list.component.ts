import { SelectionModel } from '@angular/cdk/collections';
import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { User } from 'src/app/models/user';
import { ConfirmationDialogComponent } from 'src/app/shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { UserService } from 'src/app/shared/user.service';
import { EditUserComponent } from '../edit-user/edit-user.component';

@Component({
  selector: 'app-users-list',
  templateUrl: './users-list.component.html',
  styleUrls: ['./users-list.component.css']
})
export class UsersListComponent implements OnInit {

  @ViewChild(MatPaginator) paginator: MatPaginator;
  selection = new SelectionModel<any>(true, []);
  dataSource: any;
  placeholder: string;
  displayedColumns = ["name", "surname", "email", "actions"];
  pageSize = 5;
  pageSizeOptions: number[] = [1, 5, 10, 20];

  constructor(private userService: UserService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.userService
      .getUsers()
      .subscribe((res: any) => {
        console.log(res);
        this.dataSource = new MatTableDataSource(res.data);
        this.dataSource.paginator = this.paginator;
        // this.placeholder = this.setPlaceholder();
      });
  }

  filterData(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  removeUserFromTable(user: User) {
    var temporary: User[] = [];
    this.dataSource.data.forEach(element => {
      if(element != user){
        temporary.push(element);
      }
    });
    this.dataSource = temporary;
  }

  onDeleteClick(user: User) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      height: "160px",
      width: "400px",
      data: { "dialogText": "Czy na pewno chcesz usunąć tego użytkownika?" }
    });
    dialogRef.afterClosed().subscribe((deleteUser: boolean) => {
      if (deleteUser) {
        this.userService.removeUser(user.id).subscribe((res) => {
          this.removeUserFromTable(user);
          this.toastr.success("Pomyślnie usunięto użytkownika.", "Usunięto użytkownika");
        },
          (err) => {
            this.toastr.error("Nie można usunąć użytkownika.", "Wystąpił błąd");
          });
      }
    });
  }

  onEditClick(user: User){
    const dialogRef = this.dialog.open(EditUserComponent, {
      width: "800px",
      maxHeight: "650px",
      data: { user: user }
    });
  }
}
