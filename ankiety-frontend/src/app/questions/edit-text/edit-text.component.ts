import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { QuestionService } from 'src/app/shared/question.service';

@Component({
  selector: 'app-edit-text',
  templateUrl: './edit-text.component.html',
  styleUrls: ['./edit-text.component.css']
})
export class EditTextComponent implements OnInit {
  question: any;
  surveyId: string;
  questionText: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, 
  // private toastr: ToastrService, 
  public dialogRef: MatDialogRef<EditTextComponent>, 
  private questionService: QuestionService) { }

  ngOnInit() {
    this.question = this.data.question;
    this.surveyId = this.data.surveyId;
    this.questionText = this.data.question.text;
  }

  onSubmit() {
    var questionCopy = JSON.parse(JSON.stringify(this.question));
    questionCopy.text = this.questionText;

    this.questionService.updateQuestion(questionCopy).subscribe(res => {
      this.question.text = this.questionText;
      // this.toastr.success("Pomyślnie zmieniono treść pytania", "Pomyślnie");
      this.dialogRef.close();
    },
      err => {
        // this.toastr.error("Nie można zaktualizować pytania, ponieważ ankieta już została wysłana", "Wystąpił błąd");
        this.questionText = this.data.question.text;
      });
  }

  textChanged() {
    return this.questionText != this.data.question.text;
  }

  onCloseClick() {
    this.dialogRef.close();
  }
}
