import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {ToastInfo} from "../dto/notifications/ToastInfo";
import {ToastType} from "../dto/notifications/ToastType";
import {ToastService} from "./toast.service";
import {SaleResponse} from "../dto/sales/SaleResponse";
import {SaleAddedResponse} from "../dto/sales/SaleAddedResponse";
import {UpdateSaleRequest} from "../dto/sales/UpdateSaleRequest";
import {AddSaleRequest} from "../dto/sales/AddSaleRequest";

@Injectable({
  providedIn: 'root'
})
export class SalesService {
  private readonly url = environment.API_URL + 'sale/';

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

  getSaleByContract(contractNumber: number): Observable<SaleResponse[]> {
    return this.http.get<SaleResponse[]>(this.url + 'contract/' + contractNumber)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  getSaleByFurniture(furnitureModel: number): Observable<SaleResponse[]> {
    return this.http.get<SaleResponse[]>(this.url + 'furniture/' + furnitureModel)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  getAll(): Observable<SaleResponse[]> {
    return this.http.get<SaleResponse[]>(this.url)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  add(AddSaleRequest: AddSaleRequest): Observable<SaleAddedResponse> {
    return this.http.post<SaleAddedResponse>(this.url, AddSaleRequest)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  update(contractNumber: number, furnitureModel: number, updateSaleRequest: UpdateSaleRequest): Observable<any> {
    return this.http.put<any>(this.url, updateSaleRequest,
      {
        params: {
          contractNumber: contractNumber,
          furnitureModel: furnitureModel
        }
      })
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  delete(contractNumber: number, furnitureModel: number): Observable<any> {
    return this.http.delete<any>(this.url, {
      params: {
        contractNumber: contractNumber,
        furnitureModel: furnitureModel
      }
    })
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }
}
