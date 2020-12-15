import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReservationListComponent } from './reservation/reservation-list/reservation-list.component';
import { ReservationCreateComponent } from './reservation/reservation-create/reservation-create.component';



const routes: Routes = [

  {path: 'list', component: ReservationListComponent},
  {path: 'edit/:id', component: ReservationListComponent},
  {path: 'create', component: ReservationCreateComponent},


  {path:'', redirectTo: '/list', pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { relativeLinkResolution: 'legacy' })],
  exports: [RouterModule]
})


export class AppRoutingModule { }
