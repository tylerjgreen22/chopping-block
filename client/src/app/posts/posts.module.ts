import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';
import { PostsComponent } from './posts.component';
import { AddComponent } from './add/add.component';
import { PostsRoutingModule } from './posts-routing.module';

@NgModule({
  declarations: [PostsComponent, AddComponent],
  imports: [CommonModule, RouterModule, SharedModule, PostsRoutingModule],
})
export class PostsModule {}
