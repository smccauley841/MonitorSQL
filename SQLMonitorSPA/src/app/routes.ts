import {Routes} from '@angular/router';
import { SqlinstancesComponent } from './sqlinstances/sqlinstances.component';
import { DatabasesComponent } from './databases/databases.component';
import { InstanceDetailsComponent } from './sqlserverinstances/instance-details/instance-details.component';

export const appRoutes: Routes = [
    {path: 'home', component: SqlinstancesComponent},
    {path: 'instances', component: SqlinstancesComponent},
    {path: 'instances/:id', component: InstanceDetailsComponent},
    {path: 'databases', component: DatabasesComponent},
    {path: '**', redirectTo: 'home', pathMatch: 'full'},
];
