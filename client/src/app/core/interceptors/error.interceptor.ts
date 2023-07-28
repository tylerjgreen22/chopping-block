import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
  HttpErrorResponse,
} from '@angular/common/http';
import { NavigationExtras, Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';

import { ToastrService } from 'ngx-toastr';

// Interceptor responsible for intercepting errors and generating appropriate error response
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  // Injecting router and toastr service to redirect on certain errors, and to create error toasts for certain errors: https://www.npmjs.com/package/ngx-toastr
  constructor(private router: Router, private toastr: ToastrService) {}

  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        if (error) {
          // If the error is 400 bad request
          if (error.status === 400) {
            // If the error is validation error (multiple errors)
            if (error.error.errors) {
              this.toastr.error(error.error.message, error.status.toString());
              this.router.navigateByUrl('/not-found');
            }
            // If the error is bad request, creates error toast
            else {
              this.toastr.error(error.error.message, error.status.toString());
              this.router.navigateByUrl('/not-found');
            }
          }

          // If the error is 401 unauthorized error, creates error toast
          if (error.status === 401) {
            this.toastr.error(error.error.message, error.status.toString());
          }

          // If the error is 404 not found, redirect to 404 page
          if (error.status === 404) {
            this.router.navigateByUrl('/not-found');
          }

          // If the error is 500 server error, redirect to server error page, uses navigationExtras to perserve error state and send to error page
          if (error.status === 500) {
            const navigationExtras: NavigationExtras = {
              state: { error: error.error },
            };
            this.router.navigateByUrl('/server-error', navigationExtras);
          }
        }
        return throwError(() => new Error(error.message));
      })
    );
  }
}
