import { Component, OnInit } from '@angular/core';


import { Reservation } from '../models/reservation.model';
import {ReservationService } from '../../Services/reservation.service';
import {SortByReservation} from '../models/SortByReservation';

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

  allSortByReservation: SortByReservation[];

  direction : boolean =false;
  column : string ='';

  constructor( public reservationService: ReservationService) { }

  ngOnInit(): void {

    this.getReservations();

    this.collectionSize = this.reservations.length;
    this.pageSize = 8;
    this.page=1;


    this.allSortByReservation = [
      new SortByReservation('dateAsc', 'by Date Ascending'),
      new SortByReservation('dateDesc', 'by Date Descending'),
      new SortByReservation('alphAsc',  'by Alphabetic Ascending'),
      new SortByReservation('alphDesc', 'by Alphabetic Descending'),
      new SortByReservation('rankAsc',  'by Ranking Ascending'),
      new SortByReservation('rankDesc', 'by Ranking Descending')
       ]



  }

  getReservations() {
    this.reservationService.getReservations().subscribe((reserv: any) => {
      this.reservations = reserv;
      console.log(this.reservations);
    });
  }

  onOptionsSelected(event){
    const value = event.target.value;


    switch(value){

    case 'dateAsc':
         this.direction = false;
         this.column = 'dateOfChange';
    break;

    case 'dateDesc':
      this.direction = true;
      this.column = 'dateOfChange';
    break;


    case 'alphAsc':
      this.direction = false;
      this.column = 'message';
    break;

   case 'alphDesc':
      this.direction = true;
      this.column = 'message';
   break;

   case 'rankAsc':
      this.direction = false;
      this.column = 'ranking';
   break;

   case 'rankDesc':
      this.direction = true;
      this.column = 'ranking';
   break;


    }


}
}
