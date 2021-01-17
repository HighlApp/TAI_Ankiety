import { Component, OnInit } from '@angular/core';
import { Subscription } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { SurveyService } from 'src/app/shared/survey.service';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-invitation-result',
  templateUrl: './invitation-result.component.html',
  styleUrls: ['./invitation-result.component.css']
})
export class InvitationResultComponent implements OnInit {
  routeSubscription: Subscription;
  invitationId: number;
  result: any;
  isLoading: boolean = true;
  loadingFile: boolean = false;
  surveyType: string;

  constructor(private route: ActivatedRoute, private surveyService: SurveyService, private http: HttpClient) { }

  ngOnInit() {
    this.routeSubscription = this.route.params.subscribe(params => {
      this.invitationId = params["id"];
      this.surveyService.getFilledSurvey(this.invitationId).subscribe(res => {
        // @ts-ignore
        this.result = res.data;
        console.log(this.result);
        this.isLoading = false;
      })
    });
  }

}
