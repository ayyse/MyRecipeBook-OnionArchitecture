import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register-component/register-component';
import { HomeComponent } from './components/user-components/home-component/home-component';
import { LoginComponent } from './components/login-component/login-component';

export const routes: Routes = [
    { path: 'home', component: HomeComponent },
    { path: '', redirectTo: '/home', pathMatch: 'full' }, // default route
    { path: 'register', component: RegisterComponent },
    { path: 'login', component: LoginComponent },
];
