import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms'


import { IContactType } from 'src/app/models/IContactType';
import { ContactTypeService } from 'src/app/Services/contactType.service'
import { ContactService } from 'src/app/Services/contact.service'
import { IContactView } from 'src/app/models/IContactView';
import { CustomValidatorsService } from 'src/app/Validators/custom-validators.service'

import { InsertReservationRequest } from 'src/app/models/InsertReservationRequest'
import { ReservationService } from 'src/app/Services/reservation.service';

import { ResponseReservationRequest } from 'src/app/models/ResponseReservatoinRequest';

@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})



export class ReservationCreateComponent implements OnInit {


  selectedCountries: any[];


  contactTypes: IContactType[];
  contacts: IContactView[];

  submitted = false;


  response: ResponseReservationRequest;

  property = {
    ref_no: '',
    address: '',
    manager: undefined
  };

  contactForm = new FormGroup(
    {
      contactName: new FormControl(),
      contactPhone: new FormControl(),
      contactBirthdate: new FormControl(),
      contactTypeId: new FormControl(),
      formControlName: new FormControl(),
      message: new FormControl(),
      contactId: new FormControl()
    }
  );

  controlNameContent = 'Lorem ipsum molestie rhoncus orci faucibus habitasse sociosqu rhoncus taciti, platea nam aliquam eu ultrices aliquet hendrerit. ullamcorper suscipit egestas himenaeos tincidunt quisque netus aptent bibendum, mollis eleifend fringilla platea tellus primis mattis eget, facilisis nunc ac faucibus ut justo dictumst. non lobortis quisque a pharetra duis faucibus, luctus augue sollicitudin hac rutrum. fusce per lobortis amet in auctor aliquam sed, consectetur ipsum augue aliquam felis tristique egestas facilisis, neque etiam fermentum nibh fermentum ac. imperdiet felis ut nam hendrerit curae eleifend habitasse et aliquam odio, metus rhoncus molestie risus pellentesque nam egestas augue enim condimentum, euismod ad consectetur nec taciti ut suspendisse tristique etiam."'
  contactTypeName = 'contactTypeName';

  insertReservationRequest: InsertReservationRequest;



  constructor(
    private contactTypeService: ContactTypeService,
    private contactService: ContactService,
    private fb: FormBuilder,
    private customValidator: CustomValidatorsService,
    private reservationService: ReservationService
  ) { }

  ngOnInit() {

    this.getContactTypes();

    this.getContacts();

    this.contactForm = this.fb.group({
      contactName: ['', Validators.required],
      contactPhone: ['', Validators.required],
      contactBirthdate: ['', Validators.required],
      contactTypeId: ['Contact Type', Validators.required],
      message: this.controlNameContent,
      contactId: ''
    });


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


  onFormSubmit() {
    {
      this.submitted = true;
      if (this.contactForm.valid) {

        let contact = this.contacts.find(
          contact => this.contactForm.controls['contactName'].value);

        this.contactForm.patchValue({ contactId: contact.contactId });

        this.reservationService.CreateReservation(this.contactForm.value).subscribe(
          b => alert(`Reservation created Successfully`),
          err => alert(`Exception While Updating: ${err}`)

        );

      } else {

        this.contactForm.patchValue({ contactName: '' });
        this.contactForm.patchValue({ contactTypeId: '0' });
        this.contactForm.patchValue({ contactTypeName: '' });

        this.contactForm.patchValue({ contactPhone: '' });
        this.contactForm.patchValue({ contactBirthdate: '' });

      }
    }

  }

  get registerFormControl() {
    return this.contactForm.controls;
  }

  keyword = 'contactName';

  selectEvent(contact) {
    // Select contactType, phone and birthdate.

    if (contact == null) {

      this.contactForm.patchValue({ contactTypeId: '0' });
      this.contactForm.patchValue({ contactTypeName: '' });

      this.contactForm.patchValue({ contactPhone: '' });
      this.contactForm.patchValue({ contactBirthdate: '' });

      return;

    }

    this.contactForm.patchValue({ contactTypeId: contact.contactTypeId });
    this.contactForm.patchValue({ contactTypeName: contact.contactTypeName });

    this.contactForm.patchValue({ contactPhone: contact.contactPhone });
    this.contactForm.patchValue({ contactBirthdate: contact.contactBirthdate });


  }




}


