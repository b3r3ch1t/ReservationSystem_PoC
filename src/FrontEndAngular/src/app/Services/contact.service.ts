import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';


import { IContactView } from 'src/app/models/IContactView'
import { EditContactRequest } from '../models/EditContactRequest';
import { ResponseRequest } from '../models/ResponseRequest';
import { CreateContactRequest } from '../models/CreateContactRequest';

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
      .put<ResponseRequest>(`${this.urlContact}/edit`, request)
       .pipe(
        retry(2),
        catchError(this.handleError))

  }


  DeleteContact(contactId: string ) : Observable<ResponseRequest>  {

    return this.httpClient
      .delete<ResponseRequest>(`${this.urlContact}/delete/` +  contactId)
      .pipe(
        retry(2),
        catchError(this.handleError))

  }

  AddContact(request: CreateContactRequest): Observable<ResponseRequest>  {

    return this.httpClient
      .post<ResponseRequest>(`${this.urlContact}/create`, request)
       .pipe(
        retry(2),
        catchError(this.handleError))

  }
}

