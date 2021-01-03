import { Component, Input, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';

@Component({
  selector: 'app-confirmation-dialog',
  templateUrl: './confirmation-dialog.component.html',
  styleUrls: ['./confirmation-dialog.component.css']
})
export class ConfirmationDialogComponent implements OnInit {
  @Input() dialogText: string;

  constructor(@Inject(MAT_DIALOG_DATA) public data: any, 
  public dialogRef: MatDialogRef<ConfirmationDialogComponent>) { }
  ngOnInit(): void {
  }
  
  onClick(answer: boolean) {
    this.dialogRef.close(answer);
  }

  onCloseClick() {
    this.dialogRef.close();
  }
}
