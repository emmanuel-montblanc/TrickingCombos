import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { Stance } from '../Models/stance';

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

  edditStance(stanceId: string, newName: string) {
    console.log(newName);
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.put(`${this.path}stances/${encodeURIComponent(stanceId)}`, JSON.stringify(newName), { headers });
  }

  deleteStance(stanceId: string) {
    return this.httpClient.delete(`${this.path}stances/${encodeURIComponent(stanceId)}`);
  }

  getAllTransitions(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'transitions', { headers });
  }

  addTransition(name: string, stanceIds: string[]) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { name, stanceIds };
    return this.httpClient.post(this.path + 'transitions', body, { headers });
  }

}
