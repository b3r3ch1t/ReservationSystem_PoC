import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms'


import { IContactType } from 'src/app/models/IContactType';
import { ContactTypeService } from 'src/app/Services/contactType.service'
import { ContactService } from 'src/app/Services/contact.service'
import { IContactView } from 'src/app/models/IContactView';


@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})



export class ReservationCreateComponent implements OnInit {

  myControl = new FormControl();

  contactTypes: IContactType[];
  contacts: IContactView[];

  contactTypeName = 'contactTypeName';

  constructor(public contactTypeService: ContactTypeService, public contactService : ContactService ) { }

  ngOnInit() {

    this.getContactTypes();

    this.getContacts();
  }


  getContacts() {
     this.contactService.getContat().subscribe((cont: any) => {

      this.contacts = cont;

    });
  }

  getContactTypes() {
    this.contactTypeService.getContatType().subscribe((cont: any) => {

      this.contactTypes = cont;

    });
  }

}


