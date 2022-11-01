import {Injectable} from '@angular/core';
import {Buyer} from "../dto/buyers/Buyer";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {BuyerRequest} from "../dto/buyers/BuyerRequest";
import {environment} from "../../environments/environment";
import {ToastService} from "./toast.service";
import {ToastInfo} from "../dto/notifications/ToastInfo";
import {ToastType} from "../dto/notifications/ToastType";

@Injectable({
  providedIn: 'root'
})
export class BuyersService {
  private readonly url = environment.API_URL + 'buyer/';

  constructor(
    private http: HttpClient,
    private toastService: ToastService
  ) {
  }

  private errorHandler(error: HttpErrorResponse) {
    let toast: ToastInfo = {
      header: 'Ошибка!',
      body: error.message,
      type: ToastType.Error,
      delay: 15000
    };

    this.toastService.send(toast)
    return throwError(() => error.message)
  }

  getAll(): Observable<Buyer[]> {
    return this.http.get<Buyer[]>(this.url)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  getByCode(buyerCode: number): Observable<Buyer> {
    return this.http.get<Buyer>(this.url + buyerCode)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  add(buyer: BuyerRequest): Observable<Buyer> {
    return this.http.post<Buyer>(this.url, buyer)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  update(buyerCode: number, buyer: BuyerRequest): Observable<any> {
    return this.http.put<any>(this.url + buyerCode, buyer)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  delete(buyerCode: number): Observable<any> {
    return this.http.delete<any>(this.url + buyerCode)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }
}
