import {ToastType} from "./ToastType";

export interface ToastInfo {
  header: string;
  body: string;
  type: ToastType
  delay?: number;
}
