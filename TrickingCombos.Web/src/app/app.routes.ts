import { Routes } from '@angular/router';
import { StancePageComponent } from './Components/stance-page/stance-page.component';
import { VariationPageComponent } from './Components/variation-page/variation-page.component';
import { TransitionPageComponent } from './Components/transition-page/transition-page.component';
import { TrickPageComponent } from './Components/trick-page/trick-page.component';
import { HomePageComponent } from './Components/home-page/home-page.component';

export const routes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'stances', component: StancePageComponent },
    { path: 'variations', component: VariationPageComponent },
    { path: 'transitions', component: TransitionPageComponent },
    { path: 'tricks', component: TrickPageComponent }
];