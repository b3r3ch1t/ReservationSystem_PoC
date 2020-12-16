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


import {NgbPaginationModule, NgbAlertModule} from '@ng-bootstrap/ng-bootstrap';


import {RatingModule} from 'primeng/rating';
import { AutoCompleteModule } from 'primeng/autocomplete';
import {DropdownModule} from 'primeng/dropdown';
import {InputMaskModule} from 'primeng/inputmask';
import {CalendarModule} from 'primeng/calendar';

import { AngularEditorModule } from '@kolkov/angular-editor';
import {ToastModule} from 'primeng/toast';

@NgModule({
  imports: [
    CommonModule,
    SortPipeModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    NgbPaginationModule,
    NgbAlertModule,
    RatingModule,
    DropdownModule,
    InputMaskModule,
    CalendarModule,
    AngularEditorModule,
    ToastModule
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
