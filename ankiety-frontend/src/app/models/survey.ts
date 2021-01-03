import { Question } from "./question";

export interface Survey {
  id: string;
  name: string;
  description: string;
  type: string;
  sent: boolean;
  sentInvitations: number;
  filledInvitations: number;
  questions: Question[];
}
