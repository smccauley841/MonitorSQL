import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { SqlinstanceService } from '../_service/sqlinstance.service';
import { Sqlserverstats } from '../_models/sqlserverstats';

@Component({
  selector: 'app-sqlinstances',
  templateUrl: './sqlinstances.component.html',
  styleUrls: ['./sqlinstances.component.css']
})
export class SqlinstancesComponent implements OnInit {
  sqlserverinstances: Sqlserverstats[];

  constructor(private http: HttpClient, private sqlInstanceService: SqlinstanceService) { }

  ngOnInit() {
    // this.getSQLInstances();
    // this.getInstanceStats();
    this.loadSQLInstances();
  }

  loadSQLInstances() {
    this.sqlInstanceService.getSQLInstances().subscribe((sqlserverstats: Sqlserverstats[]) => {
      this.sqlserverinstances = sqlserverstats;
    }, error => {
      console.log(error);
    });
  }
}
