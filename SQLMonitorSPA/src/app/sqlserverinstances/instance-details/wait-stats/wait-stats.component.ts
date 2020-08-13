import { Component, OnInit } from '@angular/core';
import { SqlinstanceService } from 'src/app/_service/sqlinstance.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-wait-stats',
  templateUrl: './wait-stats.component.html',
  styleUrls: ['./wait-stats.component.css']
})
export class WaitStatsComponent implements OnInit {
  waitStats: any;
  public isWaitsCollapsed = false;

  constructor(private sqlinstanceService: SqlinstanceService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadInstanceWaitStats();
  }

  loadInstanceWaitStats() {
    this.sqlinstanceService.getInstanceWaitStats(+this.route.snapshot.params.id).subscribe((instanceWaitStats: any) => {
      this.waitStats = instanceWaitStats;
    }, error => {
      console.log(error);
    });
  }
}
