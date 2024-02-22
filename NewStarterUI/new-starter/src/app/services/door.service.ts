import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs';
import { Door } from '../interfaces/door.model';

@Injectable({
  providedIn: 'root'
})
export class DoorService {

  constructor(private httpClient: HttpClient) { }

  private _baseUrl = 'https://localhost:7043/api/door/'


  getDoor(doorId: number): Observable<any> {
    return this.httpClient.get(this._baseUrl + doorId).pipe(catchError(this.handleError));
  }

  getDoors(): Observable<any> {
    return this.httpClient.get(this._baseUrl).pipe(catchError(this.handleError));
  }


  addDoor(door: Door): Observable<any> {
    debugger;
    var temp = this.httpClient.post(this._baseUrl, door).pipe(catchError(this.handleError));
    return temp;
  }

  editDoor(door: any): Observable<any> {
    return this.httpClient.put(this._baseUrl, door).pipe(catchError(this.handleError));
  }


  deleteDoor(doorId: number): Observable<any> {
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
