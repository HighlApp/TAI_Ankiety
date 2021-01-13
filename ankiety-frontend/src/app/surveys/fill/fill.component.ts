import { Component, OnInit, OnDestroy } from '@angular/core';
import { Subscription } from 'rxjs';
import { SurveyToFill } from 'src/app/models/surveyToFill';
import { ActivatedRoute, Router } from '@angular/router';
import { SurveyService } from 'src/app/shared/survey.service';
// import { ToastrService } from 'ngx-toastr';
import { TextAnswer } from 'src/app/models/textAnswer';
import { MatDialog } from '@angular/material/dialog';
import {ConfirmationDialogComponent} from "../../shared/dialogs/confirmation-dialog/confirmation-dialog.component";


@Component({
  selector: 'app-fill',
  templateUrl: './fill.component.html',
  styleUrls: ['./fill.component.css']
})
export class FillComponent implements OnInit, OnDestroy {
  routeSubscription: Subscription;
  survey: SurveyToFill;
  invitationId: number;
  numberOfPages: number;
  progressBarWidth: string;
  timerText: string;
  timer: any;
  actualQuestions: any[];
  answers: any[];
  actualPageQuestions: any[];
  actualPageNumber: number = 1;
  loading: boolean = true;

  constructor(
      private route: ActivatedRoute,
      private surveyService: SurveyService,
      // private toastr: ToastrService,
      private router: Router,
      public dialog: MatDialog) { }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      this.invitationId = params["id"];
      this.surveyService
          .getSurveyToFill(this.invitationId)
          .subscribe((res: any) => {
            this.initVariables(res);
            //TODO naprawić toastr żeby móc wywolać tą metodę
            // this.initializeTimer(this.survey, this.toastr, this.router);
            this.initTimerText(this.survey.expirationDate);
            this.loading = false;
          });
    });
  }

  ngOnDestroy() {
    clearInterval(this.timer);
  }

  takeQuestions() {
    var questions: any[] = new Array();
    var startIndex = (this.actualPageNumber - 1) * this.survey.questionsOnPage;
    var endIndex = this.actualPageNumber * this.survey.questionsOnPage;
    var idx = startIndex;
    this.survey.questions.slice(startIndex, endIndex).forEach(x => questions.push({ question: x, id: idx++ }));
    return questions;
  }

  onNextClick() {
    this.actualPageNumber++;
    this.actualPageQuestions = this.takeQuestions();
    this.updateProgressBar();
    window.scroll(0, 0);
  }

  onPreviousClick() {
    this.actualPageNumber--;
    this.actualPageQuestions = this.takeQuestions();
    this.updateProgressBar();
    window.scroll(0, 0);
  }

  isValid() {
    if (this.answers.includes(undefined))
      return false;

    var valid = true;

    this.answers.forEach(answer => {
      if (answer instanceof TextAnswer) {
        if (answer.answerText.replace(/\s/g, "") == '')
          valid = false;
      }
    });

    return valid;
  }

  onSubmitClick() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      height: "140px",
      width: "500px",
      data: { "dialogText": "Czy na pewno chcesz zakończyć ankietę ?" }
    });

    dialogRef.afterClosed().subscribe((finish: boolean) => {
      if (finish) {
        var result = {
          invitationId: this.invitationId,
          answers: []
        };

        this.answers.forEach(element => {
          if (element instanceof TextAnswer) {
            result.answers.push(element);
          }
          else {
            for (let answer of element.selectedValue) {
              result.answers.push({ questionId: element.questionId, optionId: answer.optionId });
            }
          }
        });

        this.surveyService.postFilledSurvey(result).subscribe(res => {
          // this.toastr.success("Pomyślnie wysłano ankietę.", "Wysłano ankietę");
          console.log("Pomyślnie wysłano ankietę.");
          this.router.navigateByUrl("user");
        });
      }
    });
  }

  updateProgressBar() {
    this.progressBarWidth = Math.ceil(((100 * this.actualPageNumber) / this.numberOfPages)).toLocaleString() + "%";
  }

  initVariables(res: any) {
    this.survey = res;
    this.answers = new Array(this.survey.questions.length);
    this.numberOfPages = Math.ceil(this.survey.questions.length / this.survey.questionsOnPage);
    this.actualPageQuestions = this.takeQuestions();
    this.updateProgressBar();
  }

  onBackClick() {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      height: "160px",
      width: "450px",
      data: { "dialogText": "Czy na pewno chcesz przejść do menu głównego ? Aktualny stan zostanie utracony." }
    });

    dialogRef.afterClosed().subscribe((redirect: boolean) => {
      if (redirect) {
        this.router.navigateByUrl('user/surveys');
        clearInterval(this.timer);
      }
    });
  }

  initializeTimer(survey: any, toastr: any, router: any) {

    if (survey.expirationDate != undefined) {
      this.timer = setInterval(function () {

        var now = new Date().getTime();
        var distance = new Date(survey.expirationDate).getTime() - now;

        var days = Math.floor(distance / (1000 * 60 * 60 * 24));
        var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
        var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
        var seconds = Math.floor((distance % (1000 * 60)) / 1000);

        if (days == 0 && hours == 0) {
          if (minutes > 0)
            this.timerText = minutes.toString().padStart(2, '0') + " m " + seconds.toString().padStart(2, "0") + " s";
          else
            this.timerText = seconds.toString().padStart(2, "0") + " s";

          if ((minutes == 1 || minutes == 5) && seconds == 0) {
            var firstSentence = '';

            firstSentence = minutes == 1 ? "Pozostała minuta do wygaśnięcia ankiety." : "Pozostało 5 minut do wygaśnięcia ankiety.";

            toastr.warning(firstSentence + " Uzupełnij wszystkie odpowiedzi i zakończ przed upływem czasu.", "Ankieta wygasa");
          }

          document.getElementById("timer").innerText = this.timerText;
        }

        if (distance < 0) {
          toastr.error("Upłynął czas wypełniania ankiety. Twoje odpowiedzi nie zostały zapisane.", "Upłynął czas");
          router.navigateByUrl("user");
        }
      }, 1000);
    }
  }

  private initTimerText(expirationDate: any) {
    if (expirationDate != undefined) {
      var now = new Date().getTime();
      var distance = new Date(expirationDate).getTime() - now;

      var days = Math.floor(distance / (1000 * 60 * 60 * 24));
      var hours = Math.floor((distance % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
      var minutes = Math.floor((distance % (1000 * 60 * 60)) / (1000 * 60));
      var seconds = Math.floor((distance % (1000 * 60)) / 1000);

      if (days == 0 && hours == 0) {
        if (minutes > 0)
          this.timerText = minutes.toString().padStart(2, '0') + " m " + seconds.toString().padStart(2, "0") + " s";
        else
          this.timerText = seconds.toString().padStart(2, "0") + " s";
      }
    }
  }
}