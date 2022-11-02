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
    let start = (this.startDate.value as Date)
    let end = (this.endDate.value as Date)

    end.setHours(23, 59, 59)

    let url = environment.API_URL + 'files/report?start=' + start.toISOString() + '&end=' + end.toISOString()
    window.open(url, '_blank')
  }
}
