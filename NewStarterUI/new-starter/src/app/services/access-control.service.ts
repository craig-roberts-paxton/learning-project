import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs';
import { User } from '../interfaces/user.model';
import { Door } from '../interfaces/door.model';

@Injectable({
  providedIn: 'root'
})
export class AccessControlService {

  constructor(private httpClient: HttpClient) { }

  private _baseUrl = 'https://localhost:7043/api/accesscontrol/'


  validateAccess(doorId: number, user: User): Observable<any> {
    return this.httpClient.post(this._baseUrl + doorId, user).pipe(catchError(this.handleError));
  }


  allowAccessToDoor(doorId: number, userId: number): Observable<any> {
    debugger;
    return this.httpClient.get(this._baseUrl + "allowaccesstodoor/" + doorId + "/" + userId).pipe(catchError(this.handleError));
  }


  removeAccessToDoor(doorId: number, userId: number): Observable<any> {
    return this.httpClient.delete(this._baseUrl + doorId + "/" + userId).pipe(catchError(this.handleError));
  }


  getUsersAssignedToDoor(doorId: number): Observable<any> {
    return this.httpClient.get(this._baseUrl + "doors/" + doorId + "/users").pipe(catchError(this.handleError));
  }


  getUsersNotAssignedToDoor(doorId: number): Observable<any> {
    return this.httpClient.get(this._baseUrl + "doors/" + doorId + "/assignableusers").pipe(catchError(this.handleError));
  }

  getDoorsAssignedToUser(userId: number): Observable<any> {
    return this.httpClient.get(this._baseUrl + "users/" + userId + "/doors").pipe(catchError(this.handleError));
  }


  getAuditRecords(doorId: number | undefined, userId: number | undefined): Observable<any> {
    return this.httpClient.get(this._baseUrl + "audits/" + doorId + "/" + userId).pipe(catchError(this.handleError));
  }


  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.log(error.error.message);
    }
    else {
      console.log(error.status);
    }

    return throwError(() => "Something went wrong");
  }



}
