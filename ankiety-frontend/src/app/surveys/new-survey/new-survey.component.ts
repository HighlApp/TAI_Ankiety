import { Component, OnInit, Inject } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Survey } from "src/app/models/survey";
import { SurveyService } from 'src/app/shared/survey.service';
import { MatRadioButton, MatRadioGroup } from '@angular/material/radio';

@Component({
  selector: 'app-new-survey',
  templateUrl: './new-survey.component.html',
  styleUrls: ['./new-survey.component.css']
})
export class NewSurveyComponent implements OnInit {
  survey: Survey;

  constructor(public dialogRef: MatDialogRef<NewSurveyComponent>,
    private surveyService: SurveyService,
    // private toastr: ToastrService, 
    @Inject(MAT_DIALOG_DATA) public data: any) { }

  ngOnInit() {
    this.survey = {
      name: '',
      description: '',
      type: '',
      id: '',
      sent: false,
      questions: [],
      filledInvitations: 0,
      sentInvitations: 0
    };
  }

  onSubmit() {
    this.survey.name = this.survey.name.trim().replace(/  +/g, ' ');
    this.survey.description = this.survey.description.trim().replace(/  +/g, ' ');

    this.surveyService.postSurvey(this.survey).subscribe(res => {
      this.survey.id = res.id;
      this.data.surveys.push(this.survey);
      // this.toastr.success("Pomyślnie dodano ankietę", "Dodano ankietę");
      this.dialogRef.close();
    }, (err) => {
      // this.toastr.error("Ankieta o takiej nazwie już istnieje. Spróbuj ponownie.", "Błąd podczas dodawania ankiety");
    });
  }

  onKeyPress(event: any, element: any) {
    if (element.length == 0 && event.keyCode == 32)
      return false;

    return true;
  };

  onCloseClick() {
    this.dialogRef.close();
  }
}
