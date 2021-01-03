import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DialogData } from 'src/app/models/dialogData';

@Component({
  selector: 'app-new-question',
  templateUrl: './new-question.component.html',
  styleUrls: ['./new-question.component.css']
})
export class NewQuestionComponent implements OnInit {

  selectedOption = "MultipleChoice";
  questionTypes = [
    { value: "OneChoice", name: "Jednokrotnego wyboru" },
    { value: "MultipleChoice", name: "Wielokrotnego wyboru" },
    { value: "Text", name: "Tekstowe" }
  ];

  constructor(
    public dialogRef: MatDialogRef<NewQuestionComponent>,
    @Inject(MAT_DIALOG_DATA) public data: DialogData) { }

  ngOnInit(): void { }

  onCloseClick() {
    this.dialogRef.close();
  }
}
