import { Component, OnInit } from '@angular/core';
import { ActivatedRoute} from '@angular/router'
import { FormGroup, FormControl} from '@angular/forms'

import {ReservationService } from '../../Services/reservation.service';


@Component({
  selector: 'app-reservation-edit',
  templateUrl: './reservation-edit.component.html',
  styleUrls: ['./reservation-edit.component.scss']
})
export class ReservationEditComponent implements OnInit {


  reservationForm:FormGroup;


  constructor(private _activatedRouter : ActivatedRoute ,
    public reservationService: ReservationService,  ) {



     }

  ngOnInit() {

  }



}
