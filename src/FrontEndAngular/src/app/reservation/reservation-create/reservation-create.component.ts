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

  htmlContent = 'Lorem ipsum molestie rhoncus orci faucibus habitasse sociosqu rhoncus taciti, platea nam aliquam eu ultrices aliquet hendrerit. ullamcorper suscipit egestas himenaeos tincidunt quisque netus aptent bibendum, mollis eleifend fringilla platea tellus primis mattis eget, facilisis nunc ac faucibus ut justo dictumst. non lobortis quisque a pharetra duis faucibus, luctus augue sollicitudin hac rutrum. fusce per lobortis amet in auctor aliquam sed, consectetur ipsum augue aliquam felis tristique egestas facilisis, neque etiam fermentum nibh fermentum ac. imperdiet felis ut nam hendrerit curae eleifend habitasse et aliquam odio, metus rhoncus molestie risus pellentesque nam egestas augue enim condimentum, euismod ad consectetur nec taciti ut suspendisse tristique etiam."'


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


