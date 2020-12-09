import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { ReservationListComponent } from '../reservation/reservation-list/reservation-list.component';
import { SortPipeModule } from '../Modules/sortPipe.module'


@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    SortPipeModule
  ],
  declarations:
  [
    ReservationListComponent
  ],
  exports: [
    ReservationListComponent
  ]
})


export class ReservationModule { }
