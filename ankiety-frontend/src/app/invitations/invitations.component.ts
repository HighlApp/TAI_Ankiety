import { Component, OnInit, ViewChild } from "@angular/core";
//import { MatTableDataSource, MatSort, MatPaginator, MatDialog } from "@angular/material";
import { InvitationService } from "../shared/invitation.service";
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { ConfirmationDialogComponent } from '../shared/dialogs/confirmation-dialog/confirmation-dialog.component';

@Component({
  selector: "app-invitations",
  templateUrl: "./invitations.component.html",
  styleUrls: ["./invitations.component.css"]
})
export class InvitationsComponent {
    name= 'Zaproszenia';
}
//export class InvitationsComponent implements OnInit {
  //constructor(private invitationService: InvitationService, private router: Router, private toastr: ToastrService) { }

  //actualDate: Date;
  //invitationList: MatTableDataSource<any>;
//   displayedColumns: string[] = [
//     "name",
//     "surname",
//     "surveyName",
//     "startDate",
//     "expirationDate",
//     "status",
//     "actions"
//   ];
  //@ViewChild(MatSort) sort: MatSort;
  //@ViewChild(MatPaginator) paginator: MatPaginator;

  //ngOnInit() {
    //this.invitationService.getInvitations().subscribe((res: any) => {
      //this.invitationList = new MatTableDataSource(res);
      //this.invitationList.sort = this.sort;
      //this.invitationList.paginator = this.paginator;
      //this.actualDate = new Date();
    //});
  //}

//   filterData(filterValue: string) {
//     this.invitationList.filter = filterValue.trim().toLowerCase();
//   }

//   onInvitationClick(invitation: any) {
//     if (invitation.filledSurvey) {
//       this.router.navigate(["admin/result", invitation.id]);
//     }
//   }

//   onDeleteClick(invitation: any) {
//     const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
//       height: "140px",
//       width: "400px",
//       data: { "dialogText": "Czy na pewno chcesz usunąć zaproszenie ?" }
//     });

//     dialogRef.afterClosed().subscribe((deleteInvitation: boolean) => {
//       if (deleteInvitation) {
//         this.invitationService.deleteInvitation(invitation.id).subscribe((res) => {
//           var index = this.invitationList.data.indexOf(invitation);
//           this.invitationList.data.splice(index, 1);
//           this.invitationList._updateChangeSubscription();
//           this.toastr.success("Pomyślnie usunięto zaproszenie.", "Usunięto zaproszenie");
//         },
//           (err) => {
//             this.toastr.error("Wystąpił błąd podczas usuwania zaproszenia. Spróbuj ponownie.", "Wystąpił błąd");
//           });
//       }
//     });
//   }
//}
