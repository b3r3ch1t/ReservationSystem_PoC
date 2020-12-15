import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';

import { ReservationListComponent } from 'src/app/reservation/reservation-list/reservation-list.component';
import { SortPipeModule } from 'src/app/Modules/sortPipe.module'
import { ReservationEditComponent } from 'src/app/reservation/reservation-edit/reservation-edit.component';
import { ReservationCreateComponent } from 'src/app/reservation/reservation-create/reservation-create.component';

import { NgxMaskModule, IConfig } from 'ngx-mask'

import { FormsModule } from '@angular/forms';
import { MatAutocompleteModule } from '@angular/material/autocomplete';

export const options: Partial<IConfig> | (() => Partial<IConfig>) = null;

import { AngularEditorModule } from '@kolkov/angular-editor';

import {MatDatepickerModule} from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

import { Routes, RouterModule } from '@angular/router';


import {  TruncatePipe }   from 'src/app/Pipes/TruncatePipe';

import {AutoCompleteModule} from 'primeng/autocomplete';

@NgModule({
  imports: [
    CommonModule,
    NgbModule,
    SortPipeModule,
    NgxMaskModule.forRoot(),
    RouterModule,
    MatAutocompleteModule,
    FormsModule,
    AngularEditorModule,
    ReactiveFormsModule,
    MatDatepickerModule,
    MatNativeDateModule,
    AutoCompleteModule
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
