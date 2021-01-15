import { Component, Inject, OnInit, ViewChild } from "@angular/core";
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { User } from "src/app/models/user";
import { SelectionModel } from "@angular/cdk/collections";
import { InvitationService } from "src/app/shared/invitation.service";
import { ToastrService } from 'ngx-toastr';
import { FormControl } from '@angular/forms';
import { Invitation } from 'src/app/models/invitation';
import { MatPaginator } from "@angular/material/paginator";
import { MatTableDataSource } from "@angular/material/table";
import {InvitationRequest} from "../../../models/invitationRequest";


@Component({
  selector: 'app-send-invitations',
  templateUrl: './send-invitations.component.html',
  styleUrls: ['./send-invitations.component.css']
})
export class SendInvitationsComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  selectedValue = "list";
  dataSource: any;
  displayedColumns = ["select", "name", "surname"];
  selection = new SelectionModel<any>(true, []);
  numberOfQuestions: number;
  questionsOnPageForm: any;
  surveyId: string;
  startDate: any;
  endDate: any;
  invitations = new Array<InvitationRequest>();
  value: Date;
  placeholder: string;
  minDate: Date;
  pl: any;

  constructor(
      private invitationService: InvitationService,
      public dialogRef: MatDialogRef<SendInvitationsComponent>,
      public toastr: ToastrService,
      @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.surveyId = data.survey.id;
    this.numberOfQuestions = data.numberOfQuestions;
  }

  ngOnInit() {
    this.questionsOnPageForm = new FormControl();
    this.minDate = new Date(Date.now());
    this.initLanguage();

    this.invitationService
        .getUsersForInvitation(this.data.survey.id)
        .subscribe((res: any) => {
          console.log(res);
          this.dataSource = new MatTableDataSource(res.data);
          this.dataSource.paginator = this.paginator;
          this.placeholder = this.setPlaceholder();
        });
  }

  filterData(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  isAllSelected() {
    if (this.dataSource != null) {
      const numSelected = this.selection.selected.length;
      const numRows = this.dataSource.data.length;
      return numSelected === numRows;
    }
  }

  masterToggle() {
    if (this.dataSource != null) {
      this.isAllSelected()
          ? this.selection.clear()
          : this.dataSource.data.forEach(row => this.selection.select(row));
    }
  }

  checkboxLabel(row?: any): string {
    if (!row) {
      return `${this.isAllSelected() ? "select" : "deselect"} all`;
    }

    return `${
        this.selection.isSelected(row) ? "deselect" : "select"
    } row ${row.id + 1}`;
  }

  sendUsersInvitation() {
    this.selection.selected
        .map(x => x.id)
        .forEach(x => {
          var invitation = new InvitationRequest(
              x,
              this.surveyId,
              new Date().toISOString(),
              this.startDate,
              this.endDate,
              this.questionsOnPageForm.value,
          );
          this.invitations.push(invitation);
        });

    this.data.survey.sent = true;
    this.data.survey.sentInvitations += this.selection.selected.length;

    this.invitationService
        .sendInvitations(this.invitations)
        .subscribe(res => {
          this.toastr.success('Pomyślnie wysłano zaproszenia', 'Wysłano zaproszenia');
          this.dialogRef.close();
        });
  }


  sendInvitation() {
    if (this.dateValuesValid()) {
      this.sendUsersInvitation();
    }
  }

  initTable(res: any) {
    this.selection = new SelectionModel<any>(true, []);
    this.dataSource = new MatTableDataSource(res);
    this.dataSource.paginator = this.paginator;
  }

  onCloseClick() {
    this.dialogRef.close();
  }

  isValueValid() {
    if (this.questionsOnPageForm.value == 0)
      return false;

    if (this.questionsOnPageForm.value) {
      return (this.questionsOnPageForm.value >= 1 && this.questionsOnPageForm.value <= this.numberOfQuestions);
    }

    return true;
  }

  onKeyPress(event: any) {
    if (this.questionsOnPageForm.value == null || (this.questionsOnPageForm.value && this.questionsOnPageForm.value.length == 0))
      return (event.keyCode >= 49 && event.keyCode <= 57) || event.keyCode == 8 || (event.keyCode >= 97 && event.keyCode <= 105) ||
          event.keyCode == 37 ||
          event.keyCode == 39;

    return (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 8 || (event.keyCode >= 96 && event.keyCode <= 105) || event.keyCode == 37 ||
        event.keyCode == 39;
  };


  setPlaceholder() {
    if (this.numberOfQuestions == 1)
      this.questionsOnPageForm.value = 1;

    return this.numberOfQuestions > 1 ? "Liczba pytań na stronie 1 - " + this.numberOfQuestions.toString() + " (domyślnie " + this.numberOfQuestions + ")" :
        'Liczba pytań na stronie';
  }

  dateValuesValid() {
    var dateNow = new Date(new Date(Date.now()).setSeconds(0));
    var startDate = new Date(new Date(this.startDate).setSeconds(0));
    var endDate = new Date(new Date(this.endDate).setSeconds(0));

    if (this.startDate != null) {
      if (startDate < dateNow) {
        this.toastr.error("Niepoprawna data rozpoczęcia ankiety.");
        return false;
      }
    }

    if (this.endDate != null) {
      if (endDate < dateNow) {
        this.toastr.error("Niepoprawna data zakończenia ankiety.");
        return false;
      }

      if (endDate < new Date(dateNow.getTime() + 10 * 60000)) {
        this.toastr.error("Ankieta musi być ważna minimum 10 minut. Spróbuj ponownie.", "Niepoprawne dane");
        return false;
      }
    }

    if (this.startDate != null && this.endDate != null) {
      if (endDate < startDate) {
        this.toastr.error("Wprowadź poprawne daty.");
        return false;
      }
    }

    return true;
  }

  initLanguage() {
    this.pl = {
      firstDayOfWeek: 1,
      dayNames: ["Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota", "Niedziela"],
      dayNamesShort: ["Pon", "Wt", "Śr", "Czw", "Pt", "Sob", "Nd"],
      dayNamesMin: ["Po", "Wt", "Śr", "Cz", "Pt", "So", "Nd"],
      monthNames: ["Styczeń", "Luty", "Marzec", "Kwiecień", "Maj", "Czerwiec", "Lipiec", "Sierpień", "Wrzesień", "Październik", "Listopad", "Grudzień"],
      monthNamesShort: ["Sty", "Lut", "Mar", "Kwi", "Maj", "Cze", "Lip", "Sie", "Wrz", "Paź", "Lis", "Gru"],
      today: 'Dzisiaj',
      clear: 'Wyczyść',
      dateFormat: 'dd/mm/yy',
      weekHeader: 'Tydzień'
    };
  }
}
