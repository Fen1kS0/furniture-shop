import {Component, Inject} from '@angular/core';
import {DIALOG_DATA, DialogRef} from "@angular/cdk/dialog";

@Component({
  selector: 'app-confirm-dialog',
  templateUrl: './confirm-dialog.component.html',
  styleUrls: ['./confirm-dialog.component.css']
})
export class ConfirmDialogComponent {

  constructor(
    public dialogRef: DialogRef<boolean>,
    @Inject(DIALOG_DATA) public data: { title: string, body: string }
  ) {
  }

  confirm() {
    this.dialogRef.close(true)
  }

  cancel() {
    this.dialogRef.close(false)
  }
}
