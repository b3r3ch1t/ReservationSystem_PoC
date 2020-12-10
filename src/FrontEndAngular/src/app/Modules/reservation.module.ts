import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';


import { ReservationListComponent } from '../reservation/reservation-list/reservation-list.component';
import { SortPipeModule } from '../Modules/sortPipe.module'
import { ReservationEditComponent } from '../reservation/reservation-edit/reservation-edit.component';
import { ReservationCreateComponent } from '../reservation/reservation-create/reservation-create.component';
import {MatAutocompleteModule} from '@angular/material/autocomplete';
import { NgxMaskModule, IConfig } from 'ngx-mask'

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;


@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    SortPipeModule,
    MatAutocompleteModule,
    NgxMaskModule.forRoot(),
  ],
  declarations:
  [
    ReservationListComponent,
    ReservationEditComponent,
    ReservationCreateComponent
  ],
  exports: [
    ReservationListComponent,
    ReservationEditComponent,
    ReservationCreateComponent
  ]
})


export class ReservationModule { }
