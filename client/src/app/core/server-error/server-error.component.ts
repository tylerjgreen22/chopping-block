import { Component } from '@angular/core';
import { Router } from '@angular/router';

// Server error component redirected to on 500 server error, pulls details of error from redirect
@Component({
  selector: 'app-server-error',
  templateUrl: './server-error.component.html',
  styleUrls: ['./server-error.component.scss'],
})
export class ServerErrorComponent {
  error: any;

  constructor(private router: Router) {
    const nav = this.router.getCurrentNavigation();
    this.error = nav?.extras?.state?.['error'];
  }
}
