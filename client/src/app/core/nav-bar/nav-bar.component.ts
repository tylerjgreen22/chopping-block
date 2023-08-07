import { Component } from '@angular/core';
import { AccountService } from 'src/app/account/account.service';

//NavBar component that is fixed to the top of the page
@Component({
  selector: 'app-nav-bar',
  templateUrl: './nav-bar.component.html',
  styleUrls: ['./nav-bar.component.scss'],
})
export class NavBarComponent {
  constructor(public accountService: AccountService) {}
}
