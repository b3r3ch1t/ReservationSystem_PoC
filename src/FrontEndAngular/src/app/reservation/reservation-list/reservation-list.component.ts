import { Component, OnInit } from '@angular/core';


import { Reservation } from '../reservation.model';
import {ReservationService } from '../reservation.service';


@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.scss']
})
export class ReservationListComponent implements OnInit {

  reservations: Reservation[] = [];

  constructor( public reservationService: ReservationService) { }

  ngOnInit(): void {

    this.getReservations();
  }

  getReservations() {
    this.reservationService.getReservations().subscribe((reserv: any) => {
      this.reservations = reserv;
      console.log(this.reservations);
    });
  }
}
