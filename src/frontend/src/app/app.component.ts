import { Component, OnInit } from '@angular/core';
import { Reservation } from './models/reservation';
import { ReservationService } from './services/reservation.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'Reservation System PoC';
}
