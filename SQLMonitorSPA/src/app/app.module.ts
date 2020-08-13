import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { CollapseModule } from 'ngx-bootstrap/collapse';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';

import { AppComponent } from './app.component';
import { SqlinstancesComponent } from './sqlinstances/sqlinstances.component';
import { NavComponent } from './nav/nav.component';
import { DatabasesComponent } from './databases/databases.component';
import { appRoutes } from './routes';
import { InstanceDetailsComponent } from './sqlserverinstances/instance-details/instance-details.component';
import { WaitStatsComponent } from './sqlserverinstances/instance-details/wait-stats/wait-stats.component';
import { CpuStatsComponent } from './sqlserverinstances/instance-details/cpu-stats/cpu-stats.component';
import { RamStatsComponent } from './sqlserverinstances/instance-details/ram-stats/ram-stats.component';
import { DiskIoStatsComponent } from './sqlserverinstances/instance-details/disk-io-stats/disk-io-stats.component';


@NgModule({
   declarations: [
      AppComponent,
      SqlinstancesComponent,
      NavComponent,
      DatabasesComponent,
      InstanceDetailsComponent,
      WaitStatsComponent,
      CpuStatsComponent,
      RamStatsComponent,
      DiskIoStatsComponent
   ],
   imports: [
      BrowserModule,
      HttpClientModule,
      RouterModule.forRoot(appRoutes),
      BrowserAnimationsModule,
      CollapseModule.forRoot(),
      NgbModule
   ],
   providers: [],
   bootstrap: [
      AppComponent
   ]
})
export class AppModule { }
