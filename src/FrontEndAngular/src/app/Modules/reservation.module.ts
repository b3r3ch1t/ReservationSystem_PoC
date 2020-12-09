import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { ReservationListComponent } from '../reservation/reservation-list/reservation-list.component';
import { SortPipeModule } from '../Modules/sortPipe.module'
import { ReservationEditComponent } from '../reservation/reservation-edit/reservation-edit.component';
import { ReservationCreateComponent } from '../reservation/reservation-create/reservation-create.component';

import {MatInputModule} from '@angular/material/input';

import {MatIconModule} from '@angular/material/icon';

@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    SortPipeModule,
    MatInputModule,

    MatIconModule,
  ],
  declarations:
  [
    ReservationListComponent,
    ReservationEditComponent,
    ReservationCreateComponent
  ],
  exports: [
    ReservationListComponent,
    ReservationEditComponent
  ]
})


export class ReservationModule { }
