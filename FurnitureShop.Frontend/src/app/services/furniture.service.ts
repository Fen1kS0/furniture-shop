import {Injectable} from '@angular/core';
import {environment} from "../../environments/environment";
import {HttpClient, HttpErrorResponse} from "@angular/common/http";
import {catchError, Observable, throwError} from "rxjs";
import {Furniture} from "../dto/furniture/Furniture";
import {FurnitureRequest} from "../dto/furniture/FurnitureRequest";
import {ToastInfo} from "../dto/notifications/ToastInfo";
import {ToastType} from "../dto/notifications/ToastType";
import {ToastService} from "./toast.service";

@Injectable({
  providedIn: 'root'
})
export class FurnitureService {
  private readonly url = environment.API_URL + 'furniture';

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
    return throwError(() => error)
  }

  getAll(): Observable<Furniture[]> {
    return this.http.get<Furniture[]>(this.url)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  add(furnitureRequest: FurnitureRequest): Observable<Furniture> {
    return this.http.post<Furniture>(this.url, furnitureRequest)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  update(furnitureModel: number, furniture: FurnitureRequest): Observable<any> {
    return this.http.put<any>(`${this.url}/${furnitureModel}`, furniture)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }

  delete(furnitureModel: number): Observable<any> {
    return this.http.delete<any>(`${this.url}/${furnitureModel}`)
      .pipe(
        catchError(this.errorHandler.bind(this))
      )
  }
}
