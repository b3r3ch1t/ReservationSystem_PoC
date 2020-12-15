import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { ReservationListComponent } from 'src/app/reservation/reservation-list/reservation-list.component';
import { ReservationEditComponent } from 'src/app/reservation/reservation-edit/reservation-edit.component';
import { ReservationCreateComponent } from 'src/app/reservation/reservation-create/reservation-create.component';


import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';


import {  TruncatePipe }   from 'src/app/Pipes/truncatePipe';
import { SortPipeModule } from 'src/app/modules/sortPipe.module'

import {AutoCompleteModule} from 'primeng/autocomplete';

@NgModule({
  imports: [
    CommonModule,
    SortPipeModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
  ],
  declarations:
  [
    ReservationListComponent,
    ReservationEditComponent,
    ReservationCreateComponent,
    TruncatePipe
  ],
  exports: [
    ReservationListComponent,
    ReservationEditComponent,
    ReservationCreateComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})


export class ReservationModule { }
