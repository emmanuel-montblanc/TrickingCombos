import { Component, ViewChild } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./Components/header/header.component";
import {MatSidenav, MatSidenavModule} from '@angular/material/sidenav';
import {MatExpansionModule} from '@angular/material/expansion';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';

@Component({
  selector: 'app-root',
  imports: [
    RouterOutlet,
    HeaderComponent,
    MatSidenavModule,
    MatExpansionModule,
    MatIconModule,
    MatListModule,
    RouterLink
    ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'TrickingCombos.Web';
  @ViewChild('sidenav') sidenav!: MatSidenav;

  onToggleSidenav() {
    this.sidenav.toggle();
  }
}
