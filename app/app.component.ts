import { Component, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { Observable } from 'rxjs';
import { Stance } from './Models/stance';
import { AsyncPipe } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'TrickingCombos.Web';
  // stances$ = this.GetStances();


  // private GetStances(): Observable<Stance[]> {
  //   return this.http.get<Stance[]>("https://localhost:7025/api/Stances");
  // }
}
