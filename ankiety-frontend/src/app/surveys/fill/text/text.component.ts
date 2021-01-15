import { Component, OnInit, Input } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { TextAnswer } from 'src/app/models/textAnswer';
@Component({
  selector: 'app-text',
  templateUrl: './text.component.html',
  styleUrls: ['./text.component.css']
})
export class TextComponent implements OnInit {
  @Input() questionId: string;
  @Input() index: number;
  @Input() answers: any;
  form: FormGroup;

  constructor(private fb: FormBuilder) { }

  ngOnInit() {
    this.initForm();
  }

  initForm() {
    this.form = this.fb.group({
      text: [this.answers[this.index] != null ? this.answers[this.index].answerText : '', Validators.required]
    });

    this.form.controls["text"].valueChanges.subscribe((value) => {
      this.answers[this.index] = new TextAnswer(this.questionId, value);
    })
  }
}
