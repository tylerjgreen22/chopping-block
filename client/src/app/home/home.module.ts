import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { HomeComponent } from './home.component';
import { PostComponent } from './post/post.component';
import { SharedModule } from '../shared/shared.module';
import { PostDetailComponent } from './post-detail/post-detail.component';
import { HomeRoutingModule } from './home-routing.module';

//Home page module
@NgModule({
  declarations: [HomeComponent, PostComponent, PostDetailComponent],
  imports: [CommonModule, SharedModule, HomeRoutingModule],
})
export class HomeModule {}
