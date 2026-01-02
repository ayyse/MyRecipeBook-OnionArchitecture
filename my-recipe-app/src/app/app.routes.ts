import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register-component/register-component';
import { LoginComponent } from './components/login-component/login-component';
import { UserHomeComponent } from './components/user-components/user-home-component/user-home-component';
import { userGuard } from './guards/user-guard';
import { adminGuard } from './guards/admin-guard';
import { AdminHomeComponent } from './components/admin-components/admin-home-component/admin-home-component';

export const routes: Routes = [
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: '',
        component: UserHomeComponent,
        canActivate: [userGuard]
    },
    {
        path: 'admin',
        component: AdminHomeComponent,
        canActivate: [adminGuard]
    },
];
