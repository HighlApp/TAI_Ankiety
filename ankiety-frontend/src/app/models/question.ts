import { Option } from "./option";

export class Question {
  id: string;
  surveyId: string;
  text: string;
  questionType: string;
  options: Option[];
}
