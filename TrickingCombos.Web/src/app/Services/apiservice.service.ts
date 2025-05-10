import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../environment';
import { Stance } from '../Models/Stance';

@Injectable({
  providedIn: 'root'
})
export class ApiserviceService {
  private path = environment.apiUrl;

  constructor(private httpClient: HttpClient) { }

  // Users
  getAllUsers(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'users', { headers });
  }

  deleteUser(id: string) {
    return this.httpClient.delete(`${this.path}users/${encodeURIComponent(id)}`);
  }

  // Stances
  getAllStances(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'stances', { headers });
  }

  addStance(name: string) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.post(this.path + 'stances', JSON.stringify(name), { headers });
  }

  edditStance(id: string, newName: string) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.put(`${this.path}stances/${encodeURIComponent(id)}`, JSON.stringify(newName), { headers });
  }

  deleteStance(id: string) {
    return this.httpClient.delete(`${this.path}stances/${encodeURIComponent(id)}`);
  }

  // Transitions
  getAllTransitions(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'transitions', { headers });
  }

  addTransition(name: string, stanceIds: string[]) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { name, stanceIds };
    return this.httpClient.post(this.path + 'transitions', body, { headers });
  }

  editTransition(id: string, name: string, stanceIds: string[]) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { name, stanceIds };
    return this.httpClient.put(`${this.path}transitions/${encodeURIComponent(id)}`, body, { headers });
  }

  deleteTransition(id: string) {
    return this.httpClient.delete(`${this.path}transitions/${encodeURIComponent(id)}`);
  }

  // Variations
  getAllVariations(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'variations', { headers });
  }

  addVariation(name: string, landingstanceId: string) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { name, landingstanceId };
    return this.httpClient.post(this.path + 'variations', body, { headers });
  }

  editVariation(id: string, name: string, landingStanceId: string) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { name, landingStanceId };
    return this.httpClient.put(`${this.path}variations/${encodeURIComponent(id)}`, body, { headers });
  }

  deleteVariation(id: string) {
    return this.httpClient.delete(`${this.path}variations/${encodeURIComponent(id)}`);
  }

  // Tricks
  getAllTricks(): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    return this.httpClient.get(this.path + 'tricks', { headers });
  }

  addTrick(name: string, defaultLandingstanceId: string, transitionIds: string[], variationIds: string[]) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { 
      name,
      defaultLandingstanceId,
      transitionIds,
      variationIds
    };
    console.log(body);
    return this.httpClient.post(this.path + 'tricks', body, { headers });
  }

  editTrick(id: string, name: string, defaultLandingstanceId: string, transitionIds: string[], variationIds: string[]) {
    const headers = new HttpHeaders().set('content-type', 'application/json');
    const body = { 
      name,
      defaultLandingstanceId,
      transitionIds,
      variationIds
    };
    return this.httpClient.put(`${this.path}tricks/${encodeURIComponent(id)}`, body, { headers });
  }

  deleteTrick(id: string) {
    return this.httpClient.delete(`${this.path}tricks/${encodeURIComponent(id)}`);
  }
}
