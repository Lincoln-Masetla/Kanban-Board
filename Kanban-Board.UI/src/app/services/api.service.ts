import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

import {  Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

import { workItem } from '../models/workItem';
import { user } from '../models/user';

@Injectable({
  providedIn: 'root',
})
export class ApiService {
  private apiURL = 'http://localhost:57678/api';

  httpOptions = {
    headers: new HttpHeaders({
      'Content-Type': 'application/json',
    }),
  };

  constructor(private httpClient: HttpClient) {}

  getAll(): Observable<any> {
    return this.httpClient
      .get(this.apiURL + '/workItems/')

      .pipe(catchError(this.errorHandler));
  }

  getUsersAll(): Observable<any> {
    return this.httpClient
      .get(this.apiURL + '/users/')

      .pipe(catchError(this.errorHandler));
  }

  getStatusesAll(): Observable<any> {
    return this.httpClient
      .get(this.apiURL + '/workitems/status')

      .pipe(catchError(this.errorHandler));
  }

  create(workItem: workItem): Observable<any> {
    return this.httpClient
      .post(
        this.apiURL + '/workItems/',
        JSON.stringify(workItem),
        this.httpOptions
      )

      .pipe(catchError(this.errorHandler));
  }

  createUser(user: user): Observable<any> {
    return this.httpClient
      .post(this.apiURL + '/users/', JSON.stringify(user), this.httpOptions)

      .pipe(catchError(this.errorHandler));
  }

  find(id: number): Observable<any> {
    return this.httpClient
      .get(this.apiURL + '/workItems/' + id)

      .pipe(catchError(this.errorHandler));
  }

  getByStatus(status: number): Observable<any> {
    return this.httpClient
      .get(this.apiURL + '/workItems/status/' + status)

      .pipe(catchError(this.errorHandler));
  }

  update(id: number, workItem: workItem): Observable<any> {
    return this.httpClient
      .put(
        this.apiURL + '/workItems/' + id,
        JSON.stringify(workItem),
        this.httpOptions
      )

      .pipe(catchError(this.errorHandler));
  }

  updateUser(id: number, user: user): Observable<any> {
    return this.httpClient
      .put(this.apiURL + '/users/' + id, JSON.stringify(user), this.httpOptions)

      .pipe(catchError(this.errorHandler));
  }

  delete(id: number) {
    return this.httpClient
      .delete(this.apiURL + '/workItems/' + id, this.httpOptions)

      .pipe(catchError(this.errorHandler));
  }

  errorHandler(error: any) {
    let errorMessage = '';
    if (error.error instanceof ErrorEvent) {
      errorMessage = error.error.message;
    } else {
      errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
    }
    return throwError(errorMessage);
  }
}
