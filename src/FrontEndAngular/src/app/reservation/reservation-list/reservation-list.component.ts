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

  collectionSize :number ;

  pageSize :number;
  page :number;

  constructor( public reservationService: ReservationService) { }

  ngOnInit(): void {

    this.getReservations();

    this.collectionSize = this.reservations.length;
    this.pageSize = 8;
    this.page=1;


  }

  getReservations() {
    this.reservationService.getReservations().subscribe((reserv: any) => {
      this.reservations = reserv;
      console.log(this.reservations);
    });
  }
}
