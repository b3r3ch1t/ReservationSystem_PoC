import { Component, OnInit } from '@angular/core';
import { MessageService } from 'primeng/api';
import { IContactView } from 'src/app/models/IContactView';
import { ContactService } from 'src/app/Services/contact.service';

import {MenuItem, PrimeIcons} from 'primeng/api';

@Component({
  selector: 'app-contact-list',
  templateUrl: './contact-list.component.html',
  styleUrls: ['./contact-list.component.css'],
  providers: [MessageService]
})
export class ContactListComponent implements OnInit {

  constructor(
     private messageService: MessageService,

    private contactService: ContactService
    ) { }

  ngOnInit() {

    this.getContacts();
  }

  contacts: IContactView[];

  getContacts() {
    this.contactService.getContat().subscribe((cont: any) => {

      this.contacts = cont;

    });
  }


}
