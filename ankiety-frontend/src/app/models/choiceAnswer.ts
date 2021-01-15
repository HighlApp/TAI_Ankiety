import { Option } from './option';

export class ChoiceAnswer {
    constructor(public questionId: string, public selectedValue: Option[]) { }
} 