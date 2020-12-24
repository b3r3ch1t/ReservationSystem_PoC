import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule } from '@angular/forms';
import { CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { ReservationListComponent } from 'src/app/reservation/reservation-list/reservation-list.component';
import { SortPipeModule } from 'src/app/Modules/sortPipe.module'
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
import { HttpClient } from '@angular/common/http';


import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { ConverterDatePipe } from '../pipes/converterDate';

import { registerLocaleData } from '@angular/common';


import localeDe from '@angular/common/locales/de';

registerLocaleData(localeDe);

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
    ToastModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: httpTranslateLoader,
        deps: [HttpClient]
      }
    })
  ],
  declarations:
  [
    ReservationListComponent,
    ReservationCreateComponent,
    TruncatePipe,
    ConverterDatePipe
  ],
  exports: [
    ReservationListComponent,
    ReservationCreateComponent
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})


export class ReservationModule { }

// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
