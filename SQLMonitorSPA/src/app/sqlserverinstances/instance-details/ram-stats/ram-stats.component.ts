import { Component, OnInit } from '@angular/core';
import { Sqlserverstats } from 'src/app/_models/sqlserverstats';
import { Chart } from 'chart.js';
import { SqlinstanceService } from 'src/app/_service/sqlinstance.service';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-ram-stats',
  templateUrl: './ram-stats.component.html',
  styleUrls: ['./ram-stats.component.css']
})
export class RamStatsComponent implements OnInit {
  sqlserverstats: any[];
  timestamp = [];
  ram: number[] = [];
  chart = [];
  ramstat: Sqlserverstats;
  public isramCollapsed = false;

  constructor(private sqlinstanceService: SqlinstanceService, private route: ActivatedRoute) { }

  ngOnInit() {
    this.loadramChart();
  }


  loadramChart() {
    this.sqlinstanceService.getAllServerStats(+this.route.snapshot.params.id).subscribe(res => {
      this.sqlserverstats = res;
      res.forEach(y => {
        y.forEach(x => {
          this.timestamp.push(getDate(x.timestamp));
          this.ram.push(x.ram);
        });
      });
      this.chart = new Chart('canvas3', {
      type: 'line',
      data: {
        labels: this.timestamp,
        datasets: [
          {
            data: this.ram,
            borderColor: '#3cba9f',
            fill: false
          }
        ]
      },
      options: {
        legend: {
          display: false
        },
        scales: {
          xAxes: [{
            display: true,
            type: 'time',
            time: {
              displayFormats: {
                quarter: 'h:mm a'
              }
            }
          }],
          yAxes: [{
            display: true
          }],
        }
      }
    });
  });

    function getDate(dateVal) {
    const monthNames = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun',
    'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'
    ];

    const date1 = dateVal.split('T')[0];
    const time1 = dateVal.split('T')[1];
    const timeTokens = time1.split(':');
    const time = new Date(1970, 0, 1, timeTokens[0], timeTokens[1], timeTokens[2]);
    const date = new Date(date1);
    const getDay = date.getDate();
    const getMonth = date.getMonth() + 1;
    const getYear = date.getFullYear();
    const getHour = time.getHours();
    const getMinute = time.getMinutes();
    const getSecond = time.getSeconds();
    return getDay + '/' + getMonth + '/' + getYear + ' ' +  getHour + ':' + getMinute;
    }
}
}
