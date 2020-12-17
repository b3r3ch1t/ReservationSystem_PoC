import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReservationListComponent } from './reservation/reservation-list/reservation-list.component';
import { ReservationCreateComponent } from './reservation/reservation-create/reservation-create.component';
import { ContactListComponent } from './contact/contact.component';



const routes: Routes = [

  {path: 'reservation', component: ReservationListComponent},
  {path: 'reservation/create', component: ReservationCreateComponent},
  {path: 'contact', component: ContactListComponent},



  {path:'', redirectTo: 'reservation', pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})


export class AppRoutingModule { }
