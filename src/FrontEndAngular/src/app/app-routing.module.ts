import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ReservationListComponent } from './reservation/reservation-list/reservation-list.component';
import { ReservationEditComponent } from './reservation/reservation-edit/reservation-edit.component';
import { ReservationCreateComponent } from './reservation/reservation-create/reservation-create.component';



const routes: Routes = [

  {path: 'list', component: ReservationCreateComponent},
  {path: 'edit/:id', component: ReservationCreateComponent},
  {path: 'create', component: ReservationCreateComponent},


  {path:'', redirectTo: '/create', pathMatch:'full' }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})


export class AppRoutingModule { }
