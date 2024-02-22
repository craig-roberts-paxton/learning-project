import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs';
import { User } from '../interfaces/user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private httpClient: HttpClient) { }

  private _baseUrl = 'https://localhost:7043/api/user/'


  getUser(userId: number): Observable<any> {
    return this.httpClient.get(this._baseUrl + userId).pipe(catchError(this.handleError));
  }

  getUsers(): Observable<any> {
    return this.httpClient.get(this._baseUrl).pipe(catchError(this.handleError));
  }


  addUser(user: User): Observable<any> {
    debugger;
    var temp = this.httpClient.post(this._baseUrl, user).pipe(catchError(this.handleError));
    return temp;
  }

  editUser(door: any): Observable<any> {
    return this.httpClient.put(this._baseUrl, door).pipe(catchError(this.handleError));
  }


  deleteUser(doorId: number): Observable<any> {
    return this.httpClient.delete(this._baseUrl + doorId).pipe(catchError(this.handleError))
  }


  private handleError(error: HttpErrorResponse) {
    debugger;
    if (error.error instanceof ErrorEvent) {
      console.log(error.error.message);
    }
    else {
      console.log(error.status);
    }

    return throwError(() => "Something went wrong");
  }



}
