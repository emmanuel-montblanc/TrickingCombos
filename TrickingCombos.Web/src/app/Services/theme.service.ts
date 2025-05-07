import { Injectable, computed, effect, signal } from '@angular/core';

export interface AppTheme {
  name: 'light' | 'dark';
  icon: string;
}

const THEME_KEY = 'app_theme';

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private themes: AppTheme[] = [
    { name: 'light', icon: 'light_mode' },
    { name: 'dark', icon: 'dark_mode' }
  ];

  private appTheme = signal<'light' | 'dark'>(
    (localStorage.getItem(THEME_KEY) as 'light' | 'dark') || 'dark'
  );

  selectedTheme = computed(() =>
    this.themes.find((t) => t.name === this.appTheme())
  );

  getThemes(): AppTheme[] {
    return this.themes;
  }

  setTheme(theme: 'light' | 'dark'): void {
    this.appTheme.set(theme);
    localStorage.setItem(THEME_KEY, theme);
    console.debug('setting theme to : ', theme);
  }

  constructor() {
    effect(() => {
      const appTheme = this.appTheme();
      document.body.style.setProperty('color-scheme', appTheme);
      document.body.classList.remove('light', 'dark');
      document.body.classList.add(appTheme);
    });
  }
}