import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Router } from '@angular/router';
import { ReplaySubject, map, of } from 'rxjs';

import { environment } from 'src/environments/environment';
import { User } from '../shared/models/user';

// Injectable service that interacts with the account endpoint to retrieve data related to user account features
@Injectable({
  providedIn: 'root',
})
export class AccountService {
  baseUrl = environment.apiUrl;
  // Storing the user as a replay source with buffer size one allows multiple components to subscribe and get the latest information from the buffer regardless of subscribe time
  private currUserSource = new ReplaySubject<User | null>(1);
  currUser$ = this.currUserSource.asObservable();

  constructor(private http: HttpClient, private router: Router) {}

  /*
   Loads current user based on token passed in. If no token, sets the user replay subject to null and returns a null observable
   If there is a token, gets the user from back end using token and pipes result into an observable
   Uses map function from rxjs to set returned token in local storage, set the user replay subject to the user and return the user
  */
  loadCurrentUser(token: string | null) {
    if (token === null) {
      this.currUserSource.next(null);
      return of(null);
    }

    let headers = new HttpHeaders();
    headers = headers.set('Authorization', `Bearer ${token}`);

    return this.http.get<User>(this.baseUrl + 'account', { headers }).pipe(
      map((user) => {
        if (user) {
          localStorage.setItem('token', user.token);
          this.currUserSource.next(user);
          return user;
        } else {
          return null;
        }
      })
    );
  }

  // Login user based on form values, pipes user into observable and sets local storage and user replay subject
  login(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/login', values).pipe(
      map((user) => {
        localStorage.setItem('token', user.token);
        this.currUserSource.next(user);
      })
    );
  }

  // Register user based on form values, pipes user into observable and sets local storage and user replay subject
  register(values: any) {
    return this.http.post<User>(this.baseUrl + 'account/register', values).pipe(
      map((user) => {
        localStorage.setItem('token', user.token);
        this.currUserSource.next(user);
      })
    );
  }

  // Set user replace subject to null, removes token from local storage and sends user to home page
  logout() {
    localStorage.removeItem('token');
    this.currUserSource.next(null);
    this.router.navigateByUrl('/');
  }

  // Check the passed in email exists in back end
  checkEmailExists(email: string) {
    return this.http.get<boolean>(
      this.baseUrl + 'account/emailExists?email=' + email
    );
  }
}
