import { Component, Inject, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ConfirmationDialogComponent } from 'src/app/shared/dialogs/confirmation-dialog/confirmation-dialog.component';
import { QuestionService } from 'src/app/shared/question.service';
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-edit-choice',
  templateUrl: './edit-choice.component.html',
  styleUrls: ['./edit-choice.component.css']
})
export class EditChoiceComponent implements OnInit {

  questionForm: FormGroup;
  question: any;
  actualQuestion: any;
  survey: any;
  loading: boolean = true;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any,
    public dialogRef: MatDialogRef<EditChoiceComponent>,
    private fb: FormBuilder,
    private questionService: QuestionService,
    private toastr: ToastrService,
    public dialog: MatDialog
  ) { }
  
  ngOnInit(): void {
    this.question = this.data.question;
    this.survey = this.data.survey;
    this.initForm();
    this.loading = false;
  }

  get options() {
    return this.questionForm.get("options") as FormArray;
  }

  initForm() {
    this.questionForm = this.fb.group({
      text: new FormControl(this.question.text, Validators.required),
      questionType: [this.question.questionType],
      options: this.fb.array(this.initOptions())
    });
  }

  initOptions() {
    if (this.survey.type == "Normal") {
      var options = [];
      this.question.options.forEach(element => {
        options.push(this.fb.group({ optionText: [element.optionText, Validators.required] }))
      });
      return options;
    } else {
      var options = [];
      this.question.options.forEach(element => {
        options.push(this.fb.group({
          optionText: [element.optionText, Validators.required],
          value: [element.value, Validators.required]
        }));
      })
      return options;
    }
  }

  optionChanged() {
    if (this.questionForm.controls.text.value != this.question.text)
      return true;

    for (let i = 0; i < this.question.options.length; i++) {
      if (this.question.options[i].optionText != this.questionForm.controls.options.value[i].optionText
        || this.question.options[i].value != this.questionForm.controls.options.value[i].value)
        return true;
    }
  }

  onUpdateClick() {
    var questionCopy = JSON.parse(JSON.stringify(this.question));

    questionCopy.text = this.questionForm.value.text;

    for (let i = 0; i < this.question.options.length; i++) {
      questionCopy.options[i].optionText = this.questionForm.value.options[i].optionText;
      questionCopy.options[i].value = this.questionForm.value.options[i].value;
    }

    this.questionService.updateQuestion(questionCopy).subscribe((res) => {
      this.updateData();
      this.dialogRef.close(this.question);
      this.toastr.success("Pomyślnie zaktualizowano pytanie.", "Pomyślnie zaktualizowano");
    },
      (err) => {
        this.toastr.error("Nie można zaktualizować danych", "Wystąpił błąd");
      });
  }

  deleteOption(index: any, element: any) {
    const dialogRef = this.dialog.open(ConfirmationDialogComponent, {
      height: "160px",
      width: "500px",
      data: { "dialogText": "Czy na pewno chcesz usunąć tą opcję ?" }
    });

    dialogRef.afterClosed().subscribe((remove: boolean) => {
      if (remove) {
        this.questionService.deleteOption(this.question.options[index].optionId).subscribe((res) => {
          this.question.options = this.question.options.filter(x => x.optionId != this.question.options[index].optionId);
          this.options.removeAt(index);
          this.toastr.success("Pomyślnie usunięto opcję z ankiety.", "Pomyślnie usunięto");
        },
          (err) => {
            this.toastr.error("Nie można usunąć opcji z ankiety, ponieważ ankieta została już wysłana", "Wystąpił błąd");
          })
      }
    });
  }

  onKeyPress(event: any) {
    return (event.keyCode >= 48 && event.keyCode <= 57) ||
      event.keyCode == 8 ||
      (event.keyCode >= 96 && event.keyCode <= 105) ||
      event.keyCode == 189 ||
      event.keyCode == 109 ||
      event.keyCode == 37 ||
      event.keyCode == 39;
  };

  private updateData() {
    this.question.text = this.questionForm.value.text;

    for (let i = 0; i < this.question.options.length; i++) {
      this.question.options[i].optionText = this.questionForm.value.options[i].optionText;
      this.question.options[i].value = this.questionForm.value.options[i].value;
    }
  }

  onCloseClick() {
    this.dialogRef.close();
  }
}
