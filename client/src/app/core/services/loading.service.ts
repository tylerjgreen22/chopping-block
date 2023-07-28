import { Injectable } from '@angular/core';

import { NgxSpinnerService } from 'ngx-spinner';

// Service for loading state of application
@Injectable({
  providedIn: 'root',
})
export class LoadingService {
  loadingRequestCount = 0;

  // Injecting spinner service from Ngx spinner library: https://www.npmjs.com/package/ngx-spinner
  constructor(private spinnerService: NgxSpinnerService) {}

  // When application needs to load, increment loading counter and show loading indicator
  loading() {
    this.loadingRequestCount++;
    this.spinnerService.show(undefined, {
      type: 'ball-atom',
      bdColor: 'rgba(255, 255, 255)',
      color: '#333333',
    });
  }

  // When loading is complete, decrement loading counter and hide loading indicator if all loading operations are complete
  idle() {
    this.loadingRequestCount--;
    if (this.loadingRequestCount <= 0) {
      this.loadingRequestCount = 0;
      this.spinnerService.hide();
    }
  }
}
