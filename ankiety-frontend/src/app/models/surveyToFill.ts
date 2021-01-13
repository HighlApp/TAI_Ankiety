import { Question } from "./question";

export interface SurveyToFill {
  surveyId: number;
  questions: Question[];
  questionsOnPage: number;
  expirationDate: Date;
}
