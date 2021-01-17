import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Survey } from "../models/survey";
import { Observable } from "rxjs";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: "root"
})
export class SurveyService {
  readonly BaseURI: string;

  constructor(private http: HttpClient) {
    this.BaseURI = environment.apiUrl + '/';
  }

  postSurvey(survey: Survey): Observable<any> {
    return this.http.post<Survey>(this.BaseURI + "surveys", survey);
  }

  getSurveys(): Observable<any> {
    return this.http.get(this.BaseURI + "surveys");
  }

  getSurvey(id: number): Observable<any> {
    return this.http.get(this.BaseURI + "surveys/" + id);
  }

  getSurveyToFill(id: number) {
    return this.http.get(this.BaseURI + "surveys/fill/" + id);
  }

  postFilledSurvey(filledSurvey: any) {
    return this.http.post(this.BaseURI + "surveys/fill", filledSurvey);
  }

  deleteSurvey(id: number) {
    return this.http.delete(this.BaseURI + "surveys/" + id);
  }

  getFilledSurvey(invitationId: number) {
    return this.http.get(this.BaseURI + "surveys/filled/" + invitationId)
  }

  updateSurveyDetails(details: any) {
    return this.http.put(this.BaseURI + "surveys/", details);
  }
}
