import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";

import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { ToastModule } from "primeng/toast";
import { ContactListComponent } from "../contact/contact-list/contact-list.component";

import { AutoCompleteModule } from "primeng/autocomplete";

import {TableModule} from 'primeng/table';

import {DialogModule} from 'primeng/dialog';
import { InputMaskModule } from "primeng/inputmask";
import { CalendarModule } from "primeng/calendar";

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

    CalendarModule,
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
