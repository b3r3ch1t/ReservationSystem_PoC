import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { IContactType } from '../models/IContactType'

@Injectable({
  providedIn: 'root'
})

export class ContactTypeService {

  urlContactType = 'http://localhost:5000/api/v1/ContacType';

  // injecting  o HttpClient
  constructor(private httpClient: HttpClient) { }

  // Headers
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  //handle Erro
  handleError(error: HttpErrorResponse) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      // Erro from Client
      errorMessage = error.error.message;
    } else {

      errorMessage = `Code: ${error.status}, ` + `menssagem: ${error.message}`;
    }
    return throwError(errorMessage);
  };

  getContatType(): Observable<IContactType[]> {

    return this.httpClient.get<IContactType[]>(this.urlContactType)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }


}
