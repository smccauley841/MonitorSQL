import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-databases',
  templateUrl: './databases.component.html',
  styleUrls: ['./databases.component.css']
})
export class DatabasesComponent implements OnInit {  sqldatabases: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getDatabases();
  }

  getDatabases() {
    this.http.get('http://localhost:5000/api/database').subscribe(response => {
      this.sqldatabases = response;
    }, error => {
      console.log(error);
    });
  }
}
