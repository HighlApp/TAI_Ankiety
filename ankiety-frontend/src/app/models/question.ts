import { Option } from "./option";

export class Question {
  id: number;
  text: string;
  questionType: string;
  options: Option[];
}
