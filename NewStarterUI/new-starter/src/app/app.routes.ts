import { ModifyDoorUsersComponent } from './components/modify-door-users/modify-door-users.component';

import { Component, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { DoorsComponent } from './components/doors/doors.component';
import { UserComponent } from './components/user/user.component';
import { AuditComponent } from './components/audit/audit.component';

export const routes: Routes = [
    { path: 'doors', component: DoorsComponent },
    { path: 'users', component: UserComponent },
    { path: 'audit', component: AuditComponent },
    { path: '', redirectTo: 'doors', pathMatch: 'full' }
];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})

export class AppRoutingModule { }
