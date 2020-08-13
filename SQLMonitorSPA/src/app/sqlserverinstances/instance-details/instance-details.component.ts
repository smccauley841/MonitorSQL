import { Component, OnInit } from '@angular/core';
import { Sqlserverstats } from 'src/app/_models/sqlserverstats';
import { SqlinstanceService } from 'src/app/_service/sqlinstance.service';
import { ActivatedRoute } from '@angular/router';
import { Databases } from 'src/app/_models/databases';
import { DatabaseService } from 'src/app/_service/database.service';
@Component({
  selector: 'app-instance-details',
  templateUrl: './instance-details.component.html',
  styleUrls: ['./instance-details.component.css']
})
export class InstanceDetailsComponent implements OnInit {
  sqlserver: Sqlserverstats;
  databases: Databases[];
  instancestats: any;
  public isCollapsed = false;
  public isDetailsCollapsed = false;

  constructor(private sqlinstanceService: SqlinstanceService, private route: ActivatedRoute, private databaseservice: DatabaseService) { }

  ngOnInit() {
    this.loadSQLInstance();
    this.loadInstanceDatabases();
    this.loadInstanceStats();
  }
  // sqlinstance/4
  // + converts id from string to number
  loadSQLInstance() {
    this.sqlinstanceService.getSQLInstance(+this.route.snapshot.params.id).subscribe((sqlinstance: Sqlserverstats) => {
      this.sqlserver = sqlinstance;
    }, error => {
      console.log(error);
    });
  }
  // http://localhost:5000/api/database/byinstance/1
  loadInstanceDatabases() {
    this.databaseservice.getDatabaseByIstance(+this.route.snapshot.params.id).subscribe((databases: Databases[]) => {
      this.databases = databases;
    }, error => {
      console.log(error);
    });
  }

  loadInstanceStats() {
    this.sqlinstanceService.getInstanceStats(+this.route.snapshot.params.id).subscribe((instanceStats: any) => {
      this.instancestats = instanceStats;
    }, error => {
      console.log(error);
    });
  }
}
