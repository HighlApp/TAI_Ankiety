import { Injectable } from "@angular/core";
import { HttpClient } from "@angular/common/http";
import { Question } from "../models/question";
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: "root"
})
export class QuestionService {
  private readonly BaseURI: string;

  constructor(private http: HttpClient) {
    this.BaseURI = environment.apiUrl + '/questions';
  }

  postSurveyQuestion(question: Question) {
    return this.http.post<Question>(
      this.BaseURI,
      question
    );
  }

  deleteQuestion(id: string) {
    return this.http.delete(this.BaseURI + "/" + id);
  }

  updateQuestion(question: Question) {
    return this.http.put(this.BaseURI, question);
  }

  deleteOption(id: number) {
    return this.http.delete(this.BaseURI + "/option/" + id);
  }
}
