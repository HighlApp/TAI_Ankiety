import { Question } from "./question";

export class Survey {

  constructor() {
    this.questions = new Array<Question>();
  }

  id: string;
  name: string;
  description: string;
  type: string;
  sent: boolean;
  sentInvitations: number;
  filledInvitations: number;
  questions: Question[];
}
