import { Component, OnInit, Input } from '@angular/core';
import { Question } from 'src/app/models/question';
import { ChoiceAnswer } from 'src/app/models/choiceAnswer';

@Component({
  selector: 'app-one',
  templateUrl: './one.component.html',
  styleUrls: ['./one.component.css']
})
export class OneComponent implements OnInit {
  @Input() question: Question;
  @Input() answers: any;
  @Input() index: number;
  selectedValue: any;

  constructor() { }

  ngOnInit() {
    if (this.answers[this.index] != undefined) {
      this.selectedValue = this.answers[this.index].selectedValue[0]
    }
  }

  valueChanged(event: any) {
    this.answers[this.index] = new ChoiceAnswer(this.question.id, [event.value]);
  }
}