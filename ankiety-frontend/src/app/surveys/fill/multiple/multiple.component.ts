import { Component, OnInit, Input } from '@angular/core';
import { Question } from 'src/app/models/question';
import { FormGroup, FormBuilder, FormArray, FormControl } from '@angular/forms';
import { ChoiceAnswer } from 'src/app/models/choiceAnswer';

@Component({
  selector: 'app-multiple',
  templateUrl: './multiple.component.html',
  styleUrls: ['./multiple.component.css']
})
export class MultipleComponent implements OnInit {
  @Input() question: Question;
  @Input() answers: any;
  @Input() index: number;
  optionsFormGroup: FormGroup;
  options: any[];

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit() {
    this.initForm();
  }

  onChange(event) {
    const options = (<FormArray>(
        this.optionsFormGroup.get("options")
    )) as FormArray;

    if (event.checked) {
      options.push(new FormControl(event.source.value));
      this.answers[this.index] = new ChoiceAnswer(this.question.id, this.optionsFormGroup.value.options);
    } else {
      const i = options.controls.findIndex(x => x.value === event.source.value);
      options.removeAt(i);
      this.answers[this.index] = this.optionsFormGroup.value.options.length != 0 ?
          new ChoiceAnswer(this.question.id, this.optionsFormGroup.value.options) : undefined;
    }
  }

  initForm() {
    this.optionsFormGroup = this.formBuilder.group({
      options: this.formBuilder.array([])
    });

    setTimeout(() => {
      this.options = this.question.options;
    });

    if (this.answers != undefined) {
      const options = (<FormArray>(
          this.optionsFormGroup.get("options")
      )) as FormArray;
    }
  }

  optionChecked(option: any) {
    if (this.answers[this.index] != undefined)
      return this.answers[this.index].selectedValue.includes(option);
    else
      return false;
  }
}

