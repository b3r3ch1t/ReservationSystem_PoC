import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { IContactView } from 'src/app/models/IContactView';
import { ContactService } from 'src/app/Services/contact.service';

import { IContactType } from 'src/app/models/IContactType';
import { ContactTypeService } from 'src/app/services/contactType.service';
import { FormBuilder, FormControl, FormGroup, ValidationErrors, Validators } from '@angular/forms';
import { EditContactRequest } from '../models/EditContactRequest';
import { CreateContactRequest } from '../models/CreateContactRequest';


import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.css'],
  providers: [MessageService]
})
export class ContactListComponent implements OnInit {

  constructor(
    private messageService: MessageService,
    private contactService: ContactService,
    private contactTypeService: ContactTypeService,
    public translate: TranslateService,
    private fb: FormBuilder
  ) {

  }


  contactForm = new FormGroup(
    {
      contactName: new FormControl(),
      contactPhone: new FormControl(),
      contactBirthdate: new FormControl(),
      contactTypeId: new FormControl(),
      formControlName: new FormControl(),
      contactId: new FormControl()
    }
  );

  switchLang(lang: string) {
    this.translate.use(lang);
  }


  ngOnInit() {

    this.getContacts();

    this.getContactTypes();

    this.contactForm = this.fb.group({
      contactName: ['Contact Name ', Validators.required],
      contactPhone: ['', Validators.required],
      contactBirthdate: ['', Validators.required],
      contactTypeId: ['Contact Type ', Validators.required],
      contactId: ''
    });

  }

  contacts: IContactView[];

  contactTypes: IContactType[];

  headerMessage: string;
  selectContact: IContactView;

  selectedContactType: IContactType;

  action: string;
  getContacts() {
    this.contactService.getContat().subscribe((cont: any) => {

      this.contacts = cont;

    });
  }


  display: boolean = false;
  isEdit: boolean = false;


  showDialog(contact: IContactView, action: string) {

    let contactType = this.contactTypes.find(x => x.contactTypeId == contact.contactTypeId);

    this.selectedContactType = contactType;

    this.contactForm.patchValue({ contactName: contact.contactName });
    this.contactForm.patchValue({ contactPhone: contact.contactPhone });

    let formatDate = this.formatDate(contact.contactBirthdate);
    this.contactForm.patchValue({ contactBirthdate: formatDate });

    this.display = true;
    this.selectContact = contact;

    if (action === "edit") {
      this.headerMessage = "Edit Contact";
      this.isEdit = true;
      this.action = action;
    }


    if (action === "remove") {
      this.headerMessage = "Remove Contact"
      this.action = action;

      this.isEdit = false;
    }
  }


  getContactTypes() {
    this.contactTypeService.getContatType().subscribe((cont: any) => {

      this.contactTypes = cont;

    });
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



  onEditSubmit() {
    if (this.contactForm.valid) {

      this.contactForm.patchValue({ contactId: this.selectContact.contactId });

      let dateBirth = new Date(this.contactForm.get('contactBirthdate').value);

      let contactBirthDateDay = dateBirth.getDay();
      let contactBirthDateMonth = dateBirth.getMonth();
      let contactBirthDateYear = dateBirth.getFullYear();

      let contactName = this.contactForm.get('contactName').value;
      let contactTypeId = this.selectedContactType.contactTypeId;
      let contactPhone = this.contactForm.get('contactPhone').value


      let createReservationRequest: EditContactRequest =
      {
        contactId: this.selectContact.contactId,
        contactName: contactName,
        contactPhone: contactPhone,
        contactTypeId: contactTypeId,
        contactBirthDateDay: contactBirthDateDay,
        contactBirthDateMonth: contactBirthDateMonth,
        contactBirthDateYear: contactBirthDateYear
      };

      this.contactService
        .EditContact(createReservationRequest).subscribe(data => {
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

    this.display = false;

    this.getContacts();
  }


  onDeleteSubmit() {



    this.contactService
      .DeleteContact(this.selectContact.contactId).subscribe(data => {
        if (data.fail)
          data.messageFailure.forEach(element => {
            this.messageService.add({ severity: 'error', summary: 'Error', detail: element });
          });


        if (data.sucess)
          data.messageSuccess.forEach(element => {
            this.messageService.add({ severity: 'success', summary: 'Success', detail: element });
          });

          this.getContacts();
      });

    this.display = false;


  }

  onAddSubmit() {

    if (this.contactForm.valid) {

      let dateBirth = new Date(this.contactForm.get('contactBirthdate').value);

      let contactBirthDateDay = dateBirth.getDay();
      let contactBirthDateMonth = dateBirth.getMonth();
      let contactBirthDateYear = dateBirth.getFullYear();

      let contactName = this.contactForm.get('contactName').value;
      let contactTypeId = this.selectedContactType.contactTypeId;
      let contactPhone = this.contactForm.get('contactPhone').value


      let createContactRequest: CreateContactRequest =
      {
        contactName: contactName,
        contactPhone: contactPhone,
        contactTypeId: contactTypeId,
        contactBirthDateDay: contactBirthDateDay,
        contactBirthDateMonth: contactBirthDateMonth,
        contactBirthDateYear: contactBirthDateYear
      };

      this.contactService
        .AddContact(createContactRequest).subscribe(data => {
          if (data.fail)
            data.messageFailure.forEach(element => {
              this.messageService.add({ severity: 'error', summary: 'Error', detail: element });
            });


          if (data.sucess)
            data.messageSuccess.forEach(element => {
              this.messageService.add({ severity: 'success', summary: 'Success', detail: element });
            });

            this.getContacts();
        });



    } else {

      this.getFormValidationErrors();


      this.contactForm.patchValue({ contactName: '' });
      this.contactForm.patchValue({ contactTypeId: '0' });
      this.contactForm.patchValue({ contactTypeName: '' });

      this.contactForm.patchValue({ contactPhone: '' });
      this.contactForm.patchValue({ contactBirthdate: '' });

    }

    this.display = false;

    this.contactForm.patchValue({ contactName: '' });
    this.contactForm.patchValue({ contactTypeId: '0' });
    this.contactForm.patchValue({ contactTypeName: '' });

    this.contactForm.patchValue({ contactPhone: '' });
    this.contactForm.patchValue({ contactBirthdate: '' });


  }


  showDialogAdd() {

    this.headerMessage = "Add Contact";
    this.isEdit = true;
    this.action = "add";

    this.display = true;
  }

  hideDialog() {
    this.display = false;
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

}
