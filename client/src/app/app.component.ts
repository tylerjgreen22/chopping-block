import { Component, OnInit } from '@angular/core';
import { AccountService } from './account/account.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  title = 'Chopping Block';

  constructor(private accountService: AccountService) {}
  // Load user on page load if token is present
  ngOnInit(): void {
    this.loadCurrentUser();
  }

  loadCurrentUser() {
    const token = localStorage.getItem('token');
    this.accountService.loadCurrentUser(token).subscribe();
  }
}
