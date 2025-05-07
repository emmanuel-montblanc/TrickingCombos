import { Component, EventEmitter, Output } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import { ThemeService } from '../../Services/theme.service';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../Services/auth.service';
import {MatMenuModule} from '@angular/material/menu';

@Component({
  selector: 'app-header',
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule,
    RouterLink,
    MatIconModule,
    MatMenuModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  @Output() toggleSidenav = new EventEmitter<void>();
  isDarkMode = false;

  constructor(
    public themeService: ThemeService,
    public authService: AuthService
  ) {
    this.isDarkMode = this.themeService.selectedTheme()?.name === 'dark';
  }

  onMenuClick() {
    this.toggleSidenav.emit();
  }

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    const theme = this.isDarkMode ? 'dark' : 'light';
    this.themeService.setTheme(theme);
  }
}
