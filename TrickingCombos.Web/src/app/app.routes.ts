import { Routes } from '@angular/router';
import { StancePageComponent } from './Components/stance-page/stance-page.component';
import { VariationPageComponent } from './Components/variation-page/variation-page.component';
import { TransitionPageComponent } from './Components/transition-page/transition-page.component';
import { TrickPageComponent } from './Components/trick-page/trick-page.component';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { LoginPageComponent } from './Components/login-page/login-page.component';
import { RegisterPageComponent } from './Components/register-page/register-page.component';
import { AdminPageComponent } from './Components/admin-page/admin-page.component';
import { adminGuard } from './Core/Guards/admin.guard';

export const routes: Routes = [
    { path: '', component: HomePageComponent },
    { path: 'admin', component: AdminPageComponent, canActivate: [adminGuard], },
    { path: 'login', component: LoginPageComponent },
    { path: 'register', component: RegisterPageComponent },
    { path: 'stances', component: StancePageComponent },
    { path: 'variations', component: VariationPageComponent },
    { path: 'transitions', component: TransitionPageComponent },
    { path: 'tricks', component: TrickPageComponent }
];