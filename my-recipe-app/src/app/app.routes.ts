import { Routes } from '@angular/router';
import { RegisterComponent } from './components/register-component/register-component';
import { LoginComponent } from './components/login-component/login-component';
import { UserHomeComponent } from './components/user-components/user-home-component/user-home-component';
import { userGuard } from './guards/user-guard';
import { adminGuard } from './guards/admin-guard';
import { AdminHomeComponent } from './components/admin-components/admin-home-component/admin-home-component';
import { AdminCategoryComponent } from './components/admin-components/categories/admin-category-component/admin-category-component';
import { AdminDashboardComponent } from './components/admin-components/admin-dashboard-component/admin-dashboard-component';

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
        canActivate: [adminGuard],
        children: [
            {
                path: 'dashboard',
                component: AdminDashboardComponent
            },
            {
                path: 'categories',
                component: AdminCategoryComponent
            }
        ]
    },
];
