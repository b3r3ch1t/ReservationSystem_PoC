import { Component, OnInit } from '@angular/core';
import { IReservation } from 'src/app/models/IReservation';
import { ReservationService } from 'src/app/Services/reservation.service';
import { SortByReservation } from 'src/app/models/SortByReservation';
import { TranslateService } from '@ngx-translate/core';


@Component({
  selector: 'app-reservation-list',
  templateUrl: './reservation-list.component.html',
  styleUrls: ['./reservation-list.component.css']
})


export class ReservationListComponent implements OnInit {

  reservations: IReservation[] = [];

  collectionSize: number;

  pageSize: number;
  page: number;

  allSortByReservation: SortByReservation[];

  direction: boolean = false;
  column: string = '';

  constructor(
    public reservationService: ReservationService,

    public translate: TranslateService,
  ) {

  }

  switchLang(lang: string) {
    this.translate.use(lang);
  }

  ngOnInit(): void {

    this.getReservations();

    this.collectionSize = this.reservations.length;
    this.pageSize = 8;
    this.page = 1;


    this.translate.use('en');
    this.translate.setDefaultLang('en');
  }

  getListOfSort() {
    var result: SortByReservation[] = [];
    let byDateAscending = "";
    let byDateDescending = "";
    let byAlphabeticAscending = "";
    let byAlphabeticDescending = "";
    let byRankingAscending = "";
    let byRankingDescending = "";

    this.translate.get('ByDateAscending').subscribe(
      (res: string) => {

        byDateAscending = res;
        result.push(new SortByReservation('dateAsc', byDateAscending));
      });


    this.translate.get('ByDateDescending').subscribe(
      (res: string) => {
        byDateDescending = res;
        result.push(new SortByReservation('dateDesc', byDateDescending));
      });

    this.translate.get('ByAlphabeticAscending').subscribe(
      (res: string) => {
        byAlphabeticAscending = res;
        result.push(new SortByReservation('alphAsc', byAlphabeticAscending));
      });


    this.translate.get('ByAlphabeticDescending').subscribe(
      (res: string) => {
        byAlphabeticDescending = res;
        result.push(new SortByReservation('alphDesc', byAlphabeticDescending));
      });

    this.translate.get('ByRankingAscending').subscribe(
      (res: string) => {
        byRankingAscending = res;
        result.push(new SortByReservation('rankAsc', byRankingAscending));
      });

    this.translate.get('ByRankingDescending').subscribe(
      (res: string) => {

        byRankingDescending = res;
        result.push(new SortByReservation('rankDesc', byRankingDescending));
      });


    return result;


  }

  getReservations() {
    this.reservationService.getReservations().subscribe((reserv: any) => {
      this.reservations = reserv;
    });
  }

  onOptionsSelected(event) {

    const value = event.target.value;


    switch (value) {

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
