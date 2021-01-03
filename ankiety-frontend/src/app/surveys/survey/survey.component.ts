import { Component, Input, OnInit } from '@angular/core';
import { Router } from "@angular/router";
import { Survey } from 'src/app/models/survey';

@Component({
  selector: 'app-survey',
  templateUrl: './survey.component.html',
  styleUrls: ['./survey.component.css']
})
export class SurveyComponent implements OnInit {
  @Input() survey: Survey;

  constructor(private router: Router) { }

  ngOnInit() { }

  onDetailsClick() {
    if (this.survey.id != '0')
      this.router.navigate(["admin/survey", this.survey.id]);
  }
}
