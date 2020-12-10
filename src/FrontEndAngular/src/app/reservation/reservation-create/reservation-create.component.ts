import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms'
import { Validators, FormBuilder } from '@angular/forms';

import { IContactType } from 'src/app/models/IContactType';
import { ContactTypeService } from 'src/app/Services/contactType.service'
import { ContactService } from 'src/app/Services/contact.service'
import { IContactView } from 'src/app/models/IContactView';



import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})



export class ReservationCreateComponent implements OnInit {

  contactTypes: IContactType[];
  contacts: IContactView[];

  contactForm = new FormGroup(
    {
      contactName: new FormControl(),
      contactPhone: new FormControl(),
      // contactBirthdate: new FormControl(),
      contactTypeId: new FormControl()
    }
  );

  htmlContent = 'Lorem ipsum molestie rhoncus orci faucibus habitasse sociosqu rhoncus taciti, platea nam aliquam eu ultrices aliquet hendrerit. ullamcorper suscipit egestas himenaeos tincidunt quisque netus aptent bibendum, mollis eleifend fringilla platea tellus primis mattis eget, facilisis nunc ac faucibus ut justo dictumst. non lobortis quisque a pharetra duis faucibus, luctus augue sollicitudin hac rutrum. fusce per lobortis amet in auctor aliquam sed, consectetur ipsum augue aliquam felis tristique egestas facilisis, neque etiam fermentum nibh fermentum ac. imperdiet felis ut nam hendrerit curae eleifend habitasse et aliquam odio, metus rhoncus molestie risus pellentesque nam egestas augue enim condimentum, euismod ad consectetur nec taciti ut suspendisse tristique etiam."'


  contactTypeName = 'contactTypeName';

  constructor(public contactTypeService: ContactTypeService, public contactService: ContactService, private fb: FormBuilder) { }

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

  selectedContact(e) {

    let contact = this.contacts.filter(
      contact => contact.contactName === e);

    if (contact == null) return;

    this.contactForm.patchValue({ contactTypeId: contact[0].contactTypeId });
    this.contactForm.patchValue({ contactPhone: contact[0].contactPhone });


  }

  onFormSubmit(){

  }

}


