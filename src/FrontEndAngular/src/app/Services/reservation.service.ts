import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { retry, catchError } from 'rxjs/operators';
import { IReservation } from 'src/app/models/IReservation'
import { InsertReservationRequest } from '../models/InsertReservationRequest';

@Injectable({
  providedIn: 'root'
})

export class ReservationService {


  urlReservation = 'http://localhost:5000/api/v1/Reservation';

  // injecting  o HttpClient
  constructor(private httpClient: HttpClient) { }

  // Headers
  httpOptions = {
    headers: new HttpHeaders({ 'Content-Type': 'application/json' })
  }

  // Get All Reservations
  getReservations(): Observable<IReservation[]> {
    return this.httpClient.get<IReservation[]>(this.urlReservation)
      .pipe(
        retry(2),
        catchError(this.handleError))
  }


  // Get reservation by Id
  getReservationById(id: string): Observable<IReservation> {
    return this.httpClient.get<IReservation>(`${this.urlReservation}/${id}`)
      .pipe(
        retry(2),
        catchError(this.handleError))
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
    console.log(errorMessage);
    return throwError(errorMessage);
  };

  CreateReservation(request: InsertReservationRequest) {
    return this.httpClient.post<InsertReservationRequest>(`${this.urlReservation}/create`, request);
  }

}
