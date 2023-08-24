import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { AddComponent } from './add/add.component';
import { PostsComponent } from './posts/posts.component';

@NgModule({
  declarations: [LoginComponent, RegisterComponent, AddComponent, PostsComponent],
  imports: [CommonModule, AccountRoutingModule, SharedModule],
})
export class AccountModule {}
