import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Databases } from '../_models/databases';

@Injectable({
  providedIn: 'root'
})
export class DatabaseService {
  baseUrl = environment.apiUrl;

constructor(private http: HttpClient) { }

getDatabaseByIstance(id): Observable<Databases[]> {
  return this.http.get<Databases[]>(this.baseUrl + 'SQLFileSize/' + id);
}
}
