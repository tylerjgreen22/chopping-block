import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { PaginationComponent } from './pagination/pagination.component';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { TextInputComponent } from './text-input/text-input.component';
import { ReactiveFormsModule } from '@angular/forms';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { TextAreaComponent } from './text-area/text-area.component';
import { FeedComponent } from './feed/feed.component';
import { PostComponent } from './feed/post/post.component';
import { RouterModule } from '@angular/router';

// Shared module contains components and logic that will be shared amongst other components
@NgModule({
  declarations: [
    PaginationComponent,
    TextInputComponent,
    TextAreaComponent,
    FeedComponent,
    PostComponent,
  ],
  imports: [
    CommonModule,
    PaginationModule.forRoot(),
    ReactiveFormsModule,
    BsDropdownModule.forRoot(),
    RouterModule,
  ],
  exports: [
    PaginationModule,
    PaginationComponent,
    ReactiveFormsModule,
    BsDropdownModule,
    TextInputComponent,
    TextAreaComponent,
    FeedComponent,
  ],
})
export class SharedModule {}
