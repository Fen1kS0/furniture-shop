import { Injectable } from '@angular/core';
import {Subject} from "rxjs";
import {ToastInfo} from "../dto/notifications/ToastInfo";

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  message$ = new Subject<ToastInfo | undefined>()

  send(toastInfo: ToastInfo) {
    this.message$.next(toastInfo)
  }

  clear() {
    this.message$.next(undefined)
  }

  constructor() { }
}
