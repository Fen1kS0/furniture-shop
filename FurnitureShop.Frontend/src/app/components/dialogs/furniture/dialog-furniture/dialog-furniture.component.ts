import {Component, Inject} from '@angular/core';
import {MAT_DIALOG_DATA, MatDialogRef} from "@angular/material/dialog";
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {Furniture} from "../../../../dto/furniture/Furniture";
import {FurnitureRequest} from "../../../../dto/furniture/FurnitureRequest";

@Component({
  selector: 'app-dialog-furniture',
  templateUrl: './dialog-furniture.component.html',
  styleUrls: ['./dialog-furniture.component.css']
})
export class DialogFurnitureComponent {

  constructor(
    public dialogRef: MatDialogRef<DialogFurnitureComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { furniture: Furniture, submitLabel: string, submitCallback: (furniture: FurnitureRequest) => {} }
  ) {
    if (data.furniture) {
      this.name.setValue(data.furniture.name)
      this.price.setValue(data.furniture.price)
      this.characteristics.setValue(data.furniture.characteristics)
    }
  }

  form = new FormGroup({
    name: new FormControl<string>('', [
      Validators.required,
      Validators.minLength(3),
      Validators.maxLength(50)
    ]),
    price: new FormControl<string>('', [
      Validators.required,
      Validators.min(1)
    ]),
    characteristics: new FormControl<string>('')
  })

  get name() {
    return this.form.controls.name as FormControl
  }

  get price() {
    return this.form.controls.price as FormControl
  }

  get characteristics() {
    return this.form.controls.characteristics as FormControl
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

  getErrorMessagePrice(): string {
    if (this.price.hasError('required')) {
      return 'Поле должно быть заполнено'
    }

    if (this.price.hasError('min')) {
      return 'Цена должна быть больше 0'
    }

    return ''
  }

  submit(): void {
    let addFurniture: FurnitureRequest = {
      name: this.name.value as string,
      price: this.price.value as number,
      characteristics: this.characteristics.value as string
    }

    this.data.submitCallback(addFurniture)
  }

  close(): void {
    this.dialogRef.close();
  }
}
