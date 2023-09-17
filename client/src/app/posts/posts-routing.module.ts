import { NgModule } from '@angular/core';
import { PostsComponent } from './posts.component';
import { RouterModule, Routes } from '@angular/router';
import { AddComponent } from './add/add.component';

const routes: Routes = [
  { path: '', component: PostsComponent },
  { path: 'add', component: AddComponent },
  { path: 'update/:id', component: AddComponent },
];

@NgModule({
  declarations: [],
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class PostsRoutingModule {}
