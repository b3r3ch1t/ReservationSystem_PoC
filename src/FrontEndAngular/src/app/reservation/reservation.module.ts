import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReservationComponent } from './reservation.component';

@NgModule({
  imports: [
    CommonModule,
  ],
  declarations:
  [
    ReservationComponent,
  ],
  exports : [ReservationComponent  ]
})


export class ReservationModule { }
