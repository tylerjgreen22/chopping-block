import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor,
} from '@angular/common/http';
import { Observable, delay, finalize, identity } from 'rxjs';

import { LoadingService } from '../services/loading.service';
import { environment } from 'src/environments/environment';

// Interceptor for handling loading state
@Injectable()
export class LoadingInterceptor implements HttpInterceptor {
  // Injecting loading service for handling loading state
  constructor(private loadingService: LoadingService) {}

  // Sets loading when request comes in, sets idle when request finalizes
  intercept(
    request: HttpRequest<unknown>,
    next: HttpHandler
  ): Observable<HttpEvent<unknown>> {
    // Only set loading state if call is not too email exists
    if (!request.url.includes('emailExists')) {
      this.loadingService.loading();
    }

    return next.handle(request).pipe(
      environment.production ? identity : delay(1000),
      finalize(() => this.loadingService.idle())
    );
  }
}
