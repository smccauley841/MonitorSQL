import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Sqlinstances } from '../_models/sqlinstances';
import { Sqlserverstats } from '../_models/sqlserverstats';

@Injectable({
  providedIn: 'root'
})
export class SqlinstanceService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getSQLInstances(): Observable<Sqlserverstats[]> {
  return this.http.get<Sqlserverstats[]>(this.baseUrl + 'sqlinstances/getServerStats/');
}

getSQLInstance(id): Observable<Sqlserverstats> {
  return this.http.get<Sqlserverstats>(this.baseUrl + 'sqlinstances/getServerStats/' + id);
}

getInstanceStats(id): Observable<any> {
  return this.http.get<any>(this.baseUrl + 'InstanceStats/getInstanceStats/' + id);
}

getInstanceWaitStats(id): Observable<Sqlserverstats[]> {
  return this.http.get<Sqlserverstats[]>(this.baseUrl + 'InstanceStats/GetWaitStats/' + id);
}

getAllServerStats(id): Observable<any[]> {
  return this.http.get<any[]>(this.baseUrl + 'InstanceStats/getAllInstanceStats/' + id);
}
}
