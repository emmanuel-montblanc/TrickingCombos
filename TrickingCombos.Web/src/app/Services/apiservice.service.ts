import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';

@Injectable({
  providedIn: 'root'
})
export class ApiserviceService {
  private path = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  getAllStances(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'stances', { headers });
  }

  addStance(name: string) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.post(this.path + 'stances', JSON.stringify(name), { headers });
  }

  edditStance(originalName: string, newName: string) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.put(`${this.path}stances/${encodeURIComponent(originalName)}`,  JSON.stringify(newName), { headers });
  }
}
