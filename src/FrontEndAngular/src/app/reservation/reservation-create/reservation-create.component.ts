import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms'
import { IReservation } from '../../models/IReservation';
import { IContactType } from '../../models/IContactType';
import { ContactTypeService } from '../../Services/contactType.service'


@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})



export class ReservationCreateComponent implements OnInit {

  myControl = new FormControl();
  options: string[] = ['One', 'Two', 'Three'];

  contactTypes: IContactType[] ;

  contactTypeName  = 'contactTypeName';

  constructor(public contactTypeService: ContactTypeService  ) {  }

  ngOnInit() {
    this.getReservations();
}

getReservations() {
  this.contactTypeService.getContatType().subscribe((reserv: any) => {
    this.contactTypes = reserv;
  });

  // this.contactTypes = [
  //   {contactTypeId:"98789", contactTypeName:"contact name 1"},
  //   {contactTypeId: "88090", contactTypeName:"contact name 2 "}
  // ];



}

}
