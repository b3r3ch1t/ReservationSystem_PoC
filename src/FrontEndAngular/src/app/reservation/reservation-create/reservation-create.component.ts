import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl, Validators, FormBuilder, ValidationErrors } from '@angular/forms'

import { TranslateService } from '@ngx-translate/core';

import { IContactType } from 'src/app/models/IContactType';
import { ContactTypeService } from 'src/app/services/contactType.service'
import { ContactService } from 'src/app/Services/contact.service'
import { IContactView } from 'src/app/models/IContactView';
import { CustomValidatorsService } from 'src/app/Validators/custom-validators.service'

import { CreateReservationRequest } from 'src/app/models/CreateReservationRequest'
import { ReservationService } from 'src/app/Services/reservation.service';

import { MessageService, PrimeNGConfig } from 'primeng/api';

@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css'],
  providers: [MessageService]
})



export class ReservationCreateComponent implements OnInit {


  constructor(
    private contactTypeService: ContactTypeService,
    private contactService: ContactService,
    private fb: FormBuilder,
    private customValidator: CustomValidatorsService,
    private reservationService: ReservationService,
    private messageService: MessageService,
    public translate: TranslateService,
  ) { }


  ngOnInit() {

    this.getContactTypes();

    this.getContacts();

    this.contactForm = this.fb.group({
      contactName: ['', Validators.required],
      contactPhone: ['', Validators.required],
      contactBirthdate: ['', Validators.required],
      contactTypeId: ['', Validators.required],
      message: this.controlNameContent,
      contactId: ''
    });


    this.translate.use('en');
    this.translate.setDefaultLang('en');

  }

  contactTypes: IContactType[];
  contacts: IContactView[];


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

  insertReservationRequest: CreateReservationRequest;

  locale: string = 'en';
  switchLang(lang: string) {
    this.translate.use(lang);
    this.translate.get('primeng').subscribe(res => this.config.setTranslation(res));

    this.locale = lang;
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

  getFormValidationErrors() {

    Object.keys(this.contactForm.controls).forEach(key => {

      const controlErrors: ValidationErrors = this.contactForm.get(key).errors;
      if (controlErrors != null) {
        Object.keys(controlErrors).forEach(keyError => {


        });
      }
    });
  }


  onFormSubmit() {
    {
      if (this.contactForm.valid) {


        let contact = this.contacts.find(
          contact => this.contactForm.controls['contactName'].value);

        this.contactForm.patchValue({ contactId: contact.contactId });

        let dateBirth = new Date(this.contactForm.get('contactBirthdate').value);

        let contactBirthDateDay = dateBirth.getDay();
        let contactBirthDateMonth = dateBirth.getMonth();
        let contactBirthDateYear = dateBirth.getFullYear();

        let createReservationRequest: CreateReservationRequest =
        {
          contactId: contact.contactId,
          contactName: contact.contactName,
          contactPhone: contact.contactPhone,
          contactTypeId: contact.contactTypeId,
          contactBirthDateDay: contactBirthDateDay,
          contactBirthDateMonth: contactBirthDateMonth,
          contactBirthDateYear: contactBirthDateYear,
          message: this.contactForm.get('message').value
        };

        this.reservationService
          .CreateReservation(createReservationRequest).subscribe(data => {
            if (data.fail)
              data.messageFailure.forEach(element => {
                this.messageService.add({ severity: 'error', summary: 'Error', detail: element });
              });


            if (data.sucess)
              data.messageSuccess.forEach(element => {
                this.messageService.add({ severity: 'success', summary: 'Success', detail: element });
              });
          });



      } else {

        this.getFormValidationErrors();


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

  selectedContactType: IContactType;
  selectedContact: IContactView;

  filteredContacts: IContactView[];

  filterContact(event) {
    let filtered: any[] = [];
    let query = event.query;
    for (let i = 0; i < this.contacts.length; i++) {
      let country = this.contacts[i];
      if (country.contactName.toLowerCase().indexOf(query.toLowerCase()) > -1) {
        filtered.push(country);
      }
    }

    this.filteredContacts = filtered;
  }

  selectContact(event) {


    if (event == null) {

      this.contactForm.patchValue({ contactTypeId: '0' });
      this.contactForm.patchValue({ contactTypeName: '' });

      this.contactForm.patchValue({ contactPhone: '' });
      this.contactForm.patchValue({ contactBirthdate: '' });

      this.selectedContactType = null;
      return;

    }

    let contactType = this.contactTypes.find(x => x.contactTypeId == event.contactTypeId);

    this.selectedContactType = contactType;

    this.contactForm.patchValue({ contactPhone: event.contactPhone });

    let formatDate = this.formatDate(event.contactBirthdate);
    this.contactForm.patchValue({ contactBirthdate: formatDate });


  }



  public formatDate(date) {
    const d = new Date(date);
    let month = '' + (d.getMonth() + 1);
    let day = '' + d.getDate();
    const year = d.getFullYear();
    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;
    return [year, month, day].join('-');
  }

  clearedContact(event) {
    this.selectedContactType = null;
    this.contactForm.patchValue({ contactPhone: null });
    this.contactForm.patchValue({ contactBirthdate: null });
  }



}
