import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {BuyerRequest} from "../../../../dto/buyers/BuyerRequest";
import {Buyer} from "../../../../dto/buyers/Buyer";

@Component({
  selector: 'app-dialog-buyers',
  templateUrl: './dialog-buyer.component.html',
  styleUrls: ['./dialog-buyer.component.css']
})
export class DialogBuyerComponent {
  constructor(
    public dialogRef: MatDialogRef<DialogBuyerComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { buyer: Buyer, submitLabel: string, submitCallback: (buyer: BuyerRequest) => {} }
  ) {
    if (data.buyer) {
      this.name.setValue(data.buyer.name)
      this.address.setValue(data.buyer.address)
      this.numberPhone.setValue(data.buyer.numberPhone)
    }
  }

  form = new FormGroup({
    name: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50)
    ]),
    address: new FormControl<string>('', [
      Validators.required,
      Validators.maxLength(250)
    ]),
    numberPhone: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(10),
      Validators.maxLength(20)
    ])
  })

  get name() {
    return this.form.controls.name as FormControl
  }

  get address() {
    return this.form.controls.address as FormControl
  }

  get numberPhone() {
    return this.form.controls.numberPhone as FormControl
  }

  getErrorMessageName(): string {
    if (this.name.hasError('required')) {
      return 'Поле должно быть заполнено'
    }
    if (this.name.hasError('minlength')) {
      return 'Длина должна быть больше или равна 3'
    }

    return ''
  }

  getErrorMessageAddress(): string {
    if (this.address.hasError('required')) {
      return 'Поле должно быть заполнено'
    }

    return ''
  }

  getErrorMessageNumberPhone(): string {
    if (this.numberPhone.hasError('required')) {
      return 'Поле должно быть заполнено'
    }
    if (this.numberPhone.hasError('minlength')) {
      return 'Длина должна быть больше или равна 10'
    }

    return ''
  }

  submit(): void {
    let addBuyer: BuyerRequest = {
      name: this.name.value as string,
      address: this.address.value as string,
      numberPhone: this.numberPhone.value as string
    }

    this.data.submitCallback(addBuyer)
  }

  close(): void {
    this.dialogRef.close();
  }
}
