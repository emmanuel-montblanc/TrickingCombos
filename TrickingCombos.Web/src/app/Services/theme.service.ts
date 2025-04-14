import { Injectable, computed, effect, signal } from '@angular/core';

export interface AppTheme {
  name: 'light' | 'dark';
  icon: string;
}

@Injectable({
  providedIn: 'root',
})
export class ThemeService {
  private appTheme = signal<'light' | 'dark'>('light');

  private themes: AppTheme[] = [
    { name: 'light', icon: 'light_mode' },
    { name: 'dark', icon: 'dark_mode' }
  ];

  selectedTheme = computed(() =>
    this.themes.find((t) => t.name === this.appTheme())
  );

  getThemes(): AppTheme[] {
    return this.themes;
  }

  setTheme(theme: 'light' | 'dark'): void {
    this.appTheme.set(theme);
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