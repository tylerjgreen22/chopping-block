import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { SharedModule } from '../shared/shared.module';
import { AccountRoutingModule } from './account-routing.module';
import { AddComponent } from './add/add.component';
import { PostsComponent } from './posts/posts.component';
import { HomeModule } from '../home/home.module';

@NgModule({
  declarations: [
    LoginComponent,
    RegisterComponent,
    AddComponent,
    PostsComponent,
  ],
  imports: [CommonModule, AccountRoutingModule, SharedModule, HomeModule],
})
export class AccountModule {}
