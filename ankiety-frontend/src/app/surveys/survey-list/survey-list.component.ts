import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { Survey } from "src/app/models/survey";
import { SurveyService } from "src/app/shared/survey.service";
import { NewSurveyComponent } from '../new-survey/new-survey.component';

@Component({
  selector: 'app-survey-list',
  templateUrl: './survey-list.component.html',
  styleUrls: ['./survey-list.component.css']
})
export class SurveyListComponent implements OnInit {

  surveys: Survey[];
  noSurveys: Survey = { description: "Brak utworzonych ankiet. Naciśnij przycisk 'Dodaj ankietę', aby dodać nową ankietę", name: "Brak ankiet", id: '0', type: "", questions: [], sent: false, filledInvitations: 0, sentInvitations: 0 };
  loading: boolean = true;

  constructor(
    public dialog: MatDialog,
    private surveyService: SurveyService) { }

  ngOnInit() {
    this.surveyService.getSurveys().subscribe(res => {
      this.surveys = res.data.length != 0 ? res.data : [];
      this.loading = false;
    });
  }

  openDialog() {
    const dialogRef = this.dialog.open(NewSurveyComponent, {
      height: "600px",
      width: "600px",
      data: { surveys: this.surveys }
    });
  }

}
