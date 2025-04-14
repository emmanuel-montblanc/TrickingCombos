import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import {MatToolbarModule} from '@angular/material/toolbar';
import { ThemeService } from '../../Services/theme.service';
import {MatSlideToggleModule} from '@angular/material/slide-toggle';

@Component({
  selector: 'app-header',
  imports: [
    MatToolbarModule,
    MatIconModule,
    MatButtonModule,
    MatSlideToggleModule
    // MatMenuModule,
    // TitleCasePipe
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.scss'
})
export class HeaderComponent {
  isDarkMode = false;

  constructor(public themeService: ThemeService) {
    this.isDarkMode = this.themeService.selectedTheme()?.name === 'dark';
  }

  toggleTheme() {
    this.isDarkMode = !this.isDarkMode;
    const theme = this.isDarkMode ? 'dark' : 'light';
    this.themeService.setTheme(theme);
  }
}
