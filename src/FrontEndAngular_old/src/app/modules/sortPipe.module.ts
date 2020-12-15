import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';


import { SortPipe } from 'src/app/Pipes/sortPipe';


@NgModule({
  imports: [
    CommonModule
  ],
  declarations:
  [
      SortPipe
  ],
  exports: [
    SortPipe
  ]
})


export class SortPipeModule { }
