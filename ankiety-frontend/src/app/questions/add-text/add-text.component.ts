import { Component, Input, OnInit } from '@angular/core';
import { Question } from 'src/app/models/question';
import { Survey } from 'src/app/models/survey';
import { QuestionService } from 'src/app/shared/question.service';

@Component({
  selector: 'app-add-text',
  templateUrl: './add-text.component.html',
  styleUrls: ['./add-text.component.css']
})
export class AddTextComponent {

  constructor(private questionService: QuestionService, 
    // private toastr: ToastrService
    ) { }
  @Input() survey: Survey;
  @Input() dialogRef: any;
  textQuestion = "";

  onAddQuestion() {
    var question = new Question();
    question.text = this.textQuestion;
    question.options = [];
    question.questionType = "Text";
    this.questionService
      .postSurveyQuestion(this.survey.id, question)
      .subscribe(res => {
        // this.toastr.success("Pomyślnie dodano pytanie do ankiety.", "Dodano pytanie");
        this.dialogRef.close(res);
      }, err => {
        // this.toastr.error("Nie można dodać pytania do ankiety, która była wysłana.", "Błąd podczas dodawania pytania");
      }
      );
  }
}
