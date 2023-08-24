import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaginationComponent } from './pagination/pagination.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TextInputComponent } from './text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextAreaComponent } from './text-area/text-area.component';

// Shared module contains components and logic that will be shared amongst other components
@NgModule({
  declarations: [PaginationComponent, TextInputComponent, TextAreaComponent],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
  ],
  exports: [
    PaginationModule,
    PaginationComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    TextAreaComponent,
  ],
})
export class SharedModule {}
