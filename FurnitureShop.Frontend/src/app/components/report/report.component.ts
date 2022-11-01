import { Component, OnInit } from '@angular/core';
import {FormControl, FormGroup, Validators} from "@angular/forms";
import {environment} from "../../../environments/environment";

@Component({
  selector: 'app-report',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.css']
})
export class ReportComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  form = new FormGroup({
    startDate: new FormControl<Date>(new Date(), [
      Validators.required
    ]),
    endDate: new FormControl<Date>(new Date(), [
      Validators.required
    ])
  })

  get startDate() {
    return this.form.controls.startDate as FormControl
  }

  get endDate() {
    return this.form.controls.endDate as FormControl
  }

  download() {
    let start = (this.startDate.value as Date).toISOString()
    let end = (this.endDate.value as Date).toISOString()
    let url = environment.API_URL + 'files/report?start=' + start + '&end=' + end
    window.open(url, '_blank')
  }
}
