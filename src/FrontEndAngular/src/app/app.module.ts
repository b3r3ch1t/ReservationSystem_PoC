import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { ReservationListComponent } from './reservation/reservation-list/reservation-list.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

import {SortPipe} from './sort'


@NgModule({
  declarations: [
    AppComponent,
    ReservationListComponent,
    SortPipe
   ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
