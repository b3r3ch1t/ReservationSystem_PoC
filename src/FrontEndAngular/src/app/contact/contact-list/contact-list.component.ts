import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { IContactView } from 'src/app/models/IContactView';
import { ContactService } from 'src/app/Services/contact.service';

import { IContactType } from 'src/app/models/IContactType';
import { ContactTypeService } from 'src/app/services/contactType.service';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css'],
  providers: [MessageService]
})
export class ContactListComponent implements OnInit {

  constructor(
    private messageService: MessageService,
    private contactService: ContactService,
    private contactTypeService: ContactTypeService,

    private fb: FormBuilder
  ) { }


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
    alert("Edit==>" + this.contactForm.get('contactId').value);
  }

  onRemoveSubmit() {

    alert("Sub==>" + this.contactForm.get('contactId').value);
  }

  onAddSubmit() {

    alert("Add==>" + this.contactForm.get('contactId').value);
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

}
