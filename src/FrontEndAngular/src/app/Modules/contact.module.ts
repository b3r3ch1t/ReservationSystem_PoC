import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";
import { HttpClient, HttpClientModule } from '@angular/common/http';
import {  RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { ToastModule } from "primeng/toast";
import { ContactListComponent } from "../contact/contact.component";

import { AutoCompleteModule } from "primeng/autocomplete";

import {DropdownModule} from 'primeng/dropdown';

import {TableModule} from 'primeng/table';

import {DialogModule} from 'primeng/dialog';
import { InputMaskModule } from "primeng/inputmask";
import { CalendarModule } from "primeng/calendar";


import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';


@NgModule({
  imports: [
    CommonModule,

    ToastModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AutoCompleteModule,
    TableModule,
    DialogModule,
    InputMaskModule,
    DropdownModule,
    CalendarModule,
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
    ContactListComponent
  ]  ,
  exports: [
    ContactListComponent,
  ]
})

export class ContactModule { }

// AOT compilation support
export function httpTranslateLoader(http: HttpClient) {
  return new TranslateHttpLoader(http);
}
