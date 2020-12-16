import { CommonModule } from "@angular/common";
import { NgModule } from "@angular/core";

import { Routes, RouterModule } from '@angular/router';
import { FormsModule, ReactiveFormsModule } from "@angular/forms";

import { ToastModule } from "primeng/toast";
import { ContactListComponent } from "../contact/contact-list/contact-list.component";

import { AutoCompleteModule } from "primeng/autocomplete";


@NgModule({
  imports: [
    CommonModule,

    ToastModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    AutoCompleteModule,
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
