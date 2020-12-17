import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


import { IContactView } from 'src/app/models/IContactView'
import { EditContactRequest } from '../models/EditContactRequest';
import { ResponseRequest } from '../models/ResponseRequest';

@Injectable({
  providedIn: 'root'
})


export class ContactService {



  urlContact = 'http://localhost:5000/api/v1/Contact';

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

  getContat():Observable<IContactView[]> {

    return this.httpClient.get<IContactView[]>(this.urlContact)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }

  EditContact(request: EditContactRequest) : Observable<ResponseRequest>  {


    return this.httpClient
      .post<ResponseRequest>(`${this.urlContact}/edit`, request)
       .pipe(
        retry(2),
        catchError(this.handleError))

  }


}

