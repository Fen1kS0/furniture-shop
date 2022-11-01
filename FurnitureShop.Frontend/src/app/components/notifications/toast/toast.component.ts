import {Component, OnInit} from '@angular/core';
import {ToastService} from "../../../services/toast.service";
import {ToastInfo} from "../../../dto/notifications/ToastInfo";
import {ToastType} from "../../../dto/notifications/ToastType";

@Component({
  selector: 'app-toast',
  templateUrl: './toast.component.html',
  styleUrls: ['./toast.component.css']
})
export class ToastComponent implements OnInit {
  toasts: ToastInfo[] = [];

  styles = new Map([
    [ToastType.Info, 'border border-primary'],
    [ToastType.Success, 'border border-success'],
    [ToastType.Error, 'border border-danger']
  ]);

  constructor(public messageService: ToastService) {
  }

  ngOnInit(): void {
    this.messageService.message$.subscribe(toast => {
      if (toast) {
        this.toasts.push(toast)
      }
      else {
        this.clear()
      }
    })
  }

  remove(toast: ToastInfo) {
    this.toasts = this.toasts.filter((t) => t !== toast);
  }

  clear() {
    this.toasts.splice(0, this.toasts.length);
  }
}
