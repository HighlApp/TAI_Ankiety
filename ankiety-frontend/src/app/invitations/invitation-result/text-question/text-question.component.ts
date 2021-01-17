import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'app-text-question',
  templateUrl: './text-question.component.html',
  styleUrls: ['./text-question.component.css']
})
export class TextQuestionComponent implements OnInit {
  @Input() question: any;
  @Input() index: number;

  constructor() { }

  ngOnInit() {
  }

}