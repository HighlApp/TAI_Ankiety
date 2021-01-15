import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SurveyService } from 'src/app/shared/survey.service';
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-edit-survey',
  templateUrl: './edit-survey.component.html',
  styleUrls: ['./edit-survey.component.css']
})
export class EditSurveyComponent implements OnInit {
  title: string;
  description: string;
  surveyId: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, 
  public dialogRef: MatDialogRef<EditSurveyComponent>, 
  private surveyService: SurveyService, 
  private toastr: ToastrService
  ) { }

  ngOnInit() {
    this.title = this.data.survey.name;
    this.description = this.data.survey.description;
    this.surveyId = this.data.survey.id;
  }

  onEditClick() {
    var details = { id:this.surveyId, name: this.title.trim().replace(/  +/g, ' '), description: this.description.trim().replace(/  +/g, ' ') };
    this.surveyService.updateSurveyDetails(details).subscribe((res) => {
      this.data.survey.description = details.description;
      this.data.survey.name = details.name;
      this.toastr.success("Pomyślnie zaktualizowano dane ankiety", "Pomyślnie zaktualizowano");
      this.dialogRef.close();
    },
      (err) => {
        this.toastr.error("Nie można zmienić nazwy", "Wystąpił błąd");
        this.title = this.data.survey.name;
        this.description = this.data.survey.description;
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
