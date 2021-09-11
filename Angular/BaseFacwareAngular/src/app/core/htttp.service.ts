import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders  } from '@angular/common/http';
import { catchError, tap } from 'rxjs/operators';
import { Observable, of, throwError } from 'rxjs';

@Injectable({providedIn: 'root'})
export class ApiRestService {
  constructor(private http: HttpClient) { }

  /**
   * Build full host more endpoint call
   * @param host api host server
   * @param endPoint api endpoint
   * @param version api version endpoint
   */
   private buildPath(host: string, endPoint: string, version: string = ''): string {
    let path;
    if (version === '') {
      path = `${host}/${endPoint}`;
    } else {
      path = `${host}/${version}/${endPoint}`;
    }
    console.log(path);
    return path;
  }
  /**
   * GET call
   * @param host api host server
   * @param version api version
   * @param endPoint endpoint name
   */
  get<T>(host: string, version: string, endPoint: string): Observable<any> {


    const headers= new HttpHeaders()
  .set('content-type', 'application/json')
  .set('Access-Control-Allow-Origin', 'http://localhost:4200/')
  .set('Access-Control-Allow-Methods', 'PUT, POST, PATCH, DELETE, GET');

    return this.http.get<T>(this.buildPath(host, endPoint, version)//, { headers: headers, withCredentials: true }
    ).pipe(
      tap(res => { this.logAPIResponse('Get: ', JSON.stringify(res)); }),
      catchError(this.handleError)
    );
    // return of(data);
  }
  /**
   * Create record
   * @param host api host server
   * @param version api version
   * @param endPoint endpoint name
   * @param data post data
   */
  post(host: string, version: string, endPoint: string, data: object): Observable<any> {

    //let options = new RequestOptions({ headers: headers, withCredentials: true });
// , {withCredentials: true}
//const headers= new HttpHeaders()
//.set('X-XSRF-TOKEN', 'CfDJ8PirZVlrm61NgGiAD8-z9sKqUnb7ZsdwCrW5XR2htGsNn8Ffsw0ea4EXbYAh65Z0N_kEE_IC5DSIjBI5e4q409I60SlhtvTQpupR7Ij2Qxbna5FR_slV7McSNbu9dpfjoJ2681FzsdJwFvpVvGJXi7M')
// , { headers: headers}
return this.http.post(this.buildPath(host, endPoint, version), data).pipe(
      tap(res => { this.logAPIResponse('POST: ', JSON.stringify(res)); }),
      catchError(this.handleError)
    );
  }
  /**
   * update record
   * @param host api host server
   * @param version api version
   * @param endPoint endpoint name
   * @param data data to update
   */
  put(host: string, version: string, endPoint: string, data: object): Observable<any> {
    return this.http.put(this.buildPath(host, endPoint, version), data).pipe(
      tap(res => this.logAPIResponse('PUT: ', JSON.stringify(res))),
      catchError(this.handleError)
    );
  }
  /**
   * Delete record
   * @param host api host server
   * @param version api version
   * @param endPoint endpoint name
   */
  delete(host: string, version: string, endPoint: string): Observable<any> {
    return this.http.delete(this.buildPath(host, endPoint, version)).pipe(
      tap(res => this.logAPIResponse('POST: ', JSON.stringify(res))),
      catchError(this.handleError)
    );
  }
  /**
   * handle error events
   * @param err error object
   */
  private handleError(err: HttpErrorResponse): Observable<HttpErrorResponse> {
    // const serviceResult = new ServiceResult();
    // if (err.error instanceof ErrorEvent) {
    //   serviceResult.message = `An error ocurred ${err.error.message}`;
    // } else {
    //   serviceResult.message = `Server returned code: ${err.status}, error message is: ${err.message}`;
    // }
    return throwError(err);
  }

  /**
   * log messages
   * @param message custom mesagge
   * @param optional optional bject
   */
  logAPIResponse(message:string, optional:any) {
    // console.log(`${message}`, JSON.stringify(optional));
  }
}
