import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-one-choice-question',
  templateUrl: './one-choice-question.component.html',
  styleUrls: ['./one-choice-question.component.css']
})
export class OneChoiceQuestionComponent implements OnInit {
  @Input() question: any;
  @Input() index: number;
  selected: any;
  constructor() { }

  ngOnInit() {
    this.selected = this.question.options.find((x: { selected: boolean; }) => x.selected == true);
  }
}

