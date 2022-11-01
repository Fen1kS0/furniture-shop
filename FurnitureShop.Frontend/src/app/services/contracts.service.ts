import { Injectable } from '@angular/core';
import {catchError, Observable, throwError} from "rxjs";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {environment} from "../../environments/environment";
import {ContractResponse} from "../dto/contracts/ContractResponse";
import {ContractAddedResponse} from "../dto/contracts/ContractAddedResponse";
import {AddContractRequest} from "../dto/contracts/AddContractRequest";
import {UpdateContractRequest} from "../dto/contracts/UpdateContractRequest";
import {ToastInfo} from "../dto/notifications/ToastInfo";
import {ToastType} from "../dto/notifications/ToastType";
import {ToastService} from "./toast.service";

@Injectable({
  providedIn: 'root'
})
export class ContractsService {
  private readonly url = environment.API_URL + 'contract';

  constructor(
    private http: HttpClient,
    private toastService: ToastService
  ) {
  }

  private errorHandler(error: HttpErrorResponse) {
    let toast: ToastInfo = {
      header: 'Ошибка!',
      body: error.error.errorMessage,
      type: ToastType.Error,
      delay: 15000
    };

    this.toastService.send(toast)
    return throwError(() => error.message)
  }

  getAll(): Observable<ContractResponse[]> {
    return this.http.get<ContractResponse[]>(this.url)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  getByNumber(contractNumber: number): Observable<ContractResponse> {
    return this.http.get<ContractResponse>(`${this.url}/${contractNumber}`)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  add(addContractRequest: AddContractRequest): Observable<ContractAddedResponse> {
    return this.http.post<ContractAddedResponse>(this.url, addContractRequest)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  update(contractNumber: number, updateContractRequest: UpdateContractRequest): Observable<any> {
    return this.http.put<any>(`${this.url}/${contractNumber}`, updateContractRequest)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  delete(contractNumber: number): Observable<any> {
    return this.http.delete<any>(`${this.url}/${contractNumber}`)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }
}
