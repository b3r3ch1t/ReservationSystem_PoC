import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl} from '@angular/forms'

@Component({
  selector: 'app-reservation-create',
  templateUrl: './reservation-create.component.html',
  styleUrls: ['./reservation-create.component.css']
})
export class ReservationCreateComponent implements OnInit {


  reservationForm:FormGroup;



  constructor() { }

  ngOnInit() {

    this.reservationForm = new FormGroup(
      {
        description : new FormControl(),
        dateOfChange : new FormControl(),
        ranking : new FormControl(),
        favorited: new FormControl(),
        clientId: new  FormControl(),
        clientName : new FormControl(),
        ContactTypeId:new FormControl(),
        ContactTypeName:new FormControl(),
        ClientBirthDate :new FormControl(),
      }
    )
  }



}
