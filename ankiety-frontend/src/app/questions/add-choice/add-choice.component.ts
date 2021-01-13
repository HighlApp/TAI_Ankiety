import { CdkTextareaAutosize } from '@angular/cdk/text-field';
import { Component, Input, OnInit, SimpleChanges, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { Survey } from 'src/app/models/survey';
import { MyErrorStateMatcher } from 'src/app/shared/ErrorStateMatcher';
import { QuestionService } from 'src/app/shared/question.service';
import { ToastrService } from "ngx-toastr";

@Component({
  selector: 'app-add-choice',
  templateUrl: './add-choice.component.html',
  styleUrls: ['./add-choice.component.css']
})
export class AddChoiceComponent{
  @ViewChild("autosize") autosize: CdkTextareaAutosize;
  @Input() questionType: string;
  @Input() survey: Survey;
  @Input() dialogRef;
  questionForm: FormGroup;
  isLoading = true;
  matcher = new MyErrorStateMatcher();

  constructor(
    private fb: FormBuilder,
    private questionService: QuestionService,
    private toastr: ToastrService
  ) { }

  ngOnChanges(changes: SimpleChanges) {
    if (changes["survey"].currentValue != undefined) {
      this.isLoading = false;
      this.initForm();
    }
  }

  get options() {
    return this.questionForm.get("options") as FormArray;
  }

  initForm() {
    this.questionForm = this.fb.group({
      text: new FormControl("", Validators.required),
      questionType: [this.questionType],
      options: this.fb.array(this.initOptions())
    });
  }

  addOption() {
    if (this.survey.type == "Normal") {
      this.options.push(this.fb.group({ optionText: ["", Validators.required] }));
    } else {
      this.options.push(
        this.fb.group({
          optionText: ["", Validators.required],
          value: ["", [Validators.required]]
        })
      );
    }

    this.questionForm.setErrors = null;
  }

  deleteOption(index: number) {
    this.options.removeAt(index);
  }

  initOptions() {
    if (this.survey.type == "Normal") {
      return [
        this.fb.group({ optionText: ["", Validators.required] }),
        this.fb.group({ optionText: ["", Validators.required] })
      ];
    } else {
      return [
        this.fb.group({
          optionText: ["", Validators.required],
          value: ["", Validators.required]
        }),
        this.fb.group({
          optionText: ["", Validators.required],
          value: ["", Validators.required]
        })
      ];
    }
  }

  submitForm() {
    this.removeEmptySpaces();

    this.questionService
      .postSurveyQuestion(this.survey.id, this.questionForm.value)
      .subscribe(res => {
        res.questionType = this.questionType;
        this.toastr.success("Pomyślnie dodano pytanie do ankiety.", "Dodano pytanie");
        this.dialogRef.close(res);
      }, err => {
        this.toastr.error("Nie można dodać pytania do ankiety, która była wysłana.");
      }
      );
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

  private removeEmptySpaces() {
    this.questionForm.value.text = this.questionForm.value.text.trim().replace(/  +/g, ' ');
    this.questionForm.value.options.forEach(option => {
      option.optionText = option.optionText.trim().replace(/  +/g, ' ');
    });
  }

  onQuestionTextInput(event: any, element: any) {
    if (element.length == 0 && event.keyCode == 32)
      return false;

    return true;
  };
}
