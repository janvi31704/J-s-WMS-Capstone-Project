import { Component, OnInit }
from '@angular/core';

import { CommonModule }
from '@angular/common';

import { RouterModule }
from '@angular/router';

import { MatCardModule }
from '@angular/material/card';

import { DashboardService }
from '../services/dashboard.service';

import {AuthService} from '../services/auth.service';

import{BaseChartDirective} from 'ng2-charts';

import { ChartConfiguration ,ChartType} from 'chart.js';

import {

  Chart,

  registerables

} from 'chart.js';

Chart.register(

  ...registerables
);

@Component({
  selector: 'app-dashboard',

  standalone: true,

  imports: [
    CommonModule,
    RouterModule,
    MatCardModule,
    BaseChartDirective
  ],

  templateUrl:
    './dashboard.component.html',

  styleUrls:
    ['./dashboard.component.css']
})
export class DashboardComponent
implements OnInit {

  dashboardData: any;

  constructor(
    private dashboardService:
      DashboardService,

    private authService:
      AuthService
  ) {}

  ngOnInit(): void {

    this.dashboardService
      .getSummary()
      .subscribe({

        next: (data) => {

          this.dashboardData = data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  logout() {

    this.authService.logout();
  }

  public doughnutChartLabels:
  string[] = [

    'Present',

    'Half-Day',

    'Late'
  ];

public doughnutChartData = {

  labels: [

    'Present',

    'Half-Day',

    'Late'
  ],

  datasets: [
    {
      data: [12, 5, 3]
    }
  ]
};

public doughnutChartType:
  ChartType = 'doughnut';
}