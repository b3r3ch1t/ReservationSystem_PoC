import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { ReservationListComponent } from 'src/app/reservation/reservation-list/reservation-list.component';
import { SortPipeModule } from 'src/app/Modules/sortPipe.module'
import { ReservationEditComponent } from 'src/app/reservation/reservation-edit/reservation-edit.component';
import { ReservationCreateComponent } from 'src/app/reservation/reservation-create/reservation-create.component';

import {  TruncatePipe }   from 'src/app/Pipes/TruncatePipe';

import {AutocompleteLibModule} from 'angular-ng-autocomplete';




@NgModule({
  imports: [
    CommonModule,
    SortPipeModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AutocompleteLibModule
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
