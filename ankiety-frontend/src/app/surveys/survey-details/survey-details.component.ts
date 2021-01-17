import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Question } from 'src/app/models/question';
import { Survey } from 'src/app/models/survey';
import { EditChoiceComponent } from 'src/app/questions/edit-choice/edit-choice.component';
import { EditTextComponent } from 'src/app/questions/edit-text/edit-text.component';
import { NewQuestionComponent } from 'src/app/questions/new-question/new-question.component';
import { ConfirmationDialogComponent } from 'src/app/shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { QuestionService } from 'src/app/shared/question.service';
import { SurveyService } from 'src/app/shared/survey.service';
import { EditSurveyComponent } from '../edit-survey/edit-survey/edit-survey.component';
import { ToastrService } from "ngx-toastr";
import {SendInvitationsComponent} from "./send-invitations/send-invitations.component";

@Component({
  selector: 'app-survey-details',
  templateUrl: './survey-details.component.html',
  styleUrls: ['./survey-details.component.css']
})
export class SurveyDetailsComponent implements OnInit {
  survey: Survey;
  routeSubscription: Subscription;
  surveyId: number;
  loading: boolean = true;
  loadingFile: boolean = false;

  constructor(
    private surveyService: SurveyService,
    private questionService: QuestionService,
    private route: ActivatedRoute,
    public dialog: MatDialog,
    private toastr: ToastrService,
    private router: Router,
    private http: HttpClient
  ) { }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      this.surveyId = params["id"];
      this.surveyService.getSurvey(this.surveyId).subscribe(res => {
        this.survey = res.data;
        this.survey.questions.forEach(x => x.questionType = this.mapTypes(x.questionType));
        this.loading = false;
        this.survey.sentInvitations = 0;
        this.survey.filledInvitations = 0;
      });
    });
  }

  openAddQuestionDialog() {
    const dialogRef = this.dialog.open(NewQuestionComponent, {
      width: "800px",
      maxHeight: "800px",
      data: { survey: this.survey }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined) {
        result.data.questionType = this.mapTypes(result.data.questionType)
        this.survey.questions.push(result.data);
      }
    });
  }

  openInvitationDialog() {
    const dialogRef = this.dialog.open(SendInvitationsComponent, {
      width: "1000px",
      maxHeight: "800px",
      data: {
        survey: this.survey,
        numberOfQuestions: this.survey.questions.length
      }
    });
  }

  ngOnDestroy() {
    this.routeSubscription.unsubscribe();
  }

  onDeleteSurveyClick() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      height: "140px",
      width: "400px",
      data: { "dialogText": "Czy na pewno chcesz usunąć tą ankietę ?" }
    });

    dialogRef.afterClosed().subscribe((deleteSurvey: boolean) => {
      if (deleteSurvey) {
        this.surveyService.deleteSurvey(this.surveyId).subscribe(res => {
          this.toastr.success("Pomyślnie usunięto ankietę.", "Usuwanie ankiety");

          this.router.navigate(['admin/surveys']);
        },
          err => {
            this.toastr.error("Nie można usunąć tej ankiety, ponieważ została już wysłana.", "Usuwanie ankiety");
          })
      }
    });
  }

  private mapTypes(type: any) {
    if (type == "Text" || type == 3)
      return "Tekstowe";
    else {
      if (type == "MultipleChoice" || type == 2)
        return "Wielokrotnego wyboru";
      else
        return "Jednokrotnego wyboru";
    }
  }

  onDeleteQuestionClick(question: Question) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      height: "140px",
      width: "400px",
      data: { "dialogText": "Czy na pewno chcesz usunąć to pytanie?" }
    });

    dialogRef.afterClosed().subscribe((deleteQuestion: boolean) => {
      if (deleteQuestion) {
        this.questionService.deleteQuestion(question.id).subscribe((res) => {
          this.survey.questions.splice(this.survey.questions.indexOf(question), 1);
          this.toastr.success("Pomyślnie usunięto pytanie z ankiety.", "Usunięto pytanie");
        },
          (err) => {
            this.toastr.error("Nie można usunąć pytania z ankiety, która została już opublikowana.", "Wystąpił błąd");
          });
      }
    });
  }

  onEditSurveyClick() {
      const dialogRef = this.dialog.open(EditSurveyComponent, {
        width: "800px",
        maxHeight: "650px",
        data: { survey: this.survey }
      });
  }

  onEditQuestionClick(question: any) {
      if (question.options.length == 0) {
        const dialogRef = this.dialog.open(EditTextComponent, {
          width: "800px",
          maxHeight: "650px",
          data: { surveyId: this.surveyId, question: question }
        });
      }
      else {
        const dialogRef = this.dialog.open(EditChoiceComponent, {
          width: "800px",
          maxHeight: "650px",
          data: { survey: this.survey, question: question }
        });
      }
  }
}
