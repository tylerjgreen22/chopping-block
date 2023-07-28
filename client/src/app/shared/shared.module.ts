import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaginationComponent } from './pagination/pagination.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';

// Shared module contains components and logic that will be shared amongst other components
@NgModule({
  declarations: [PaginationComponent],
  imports: [CommonModule, PaginationModule.forRoot()],
  exports: [PaginationModule, PaginationComponent],
})
export class SharedModule {}
