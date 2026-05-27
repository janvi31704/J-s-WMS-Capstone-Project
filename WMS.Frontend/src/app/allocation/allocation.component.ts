import {
  Component,
  OnInit
}
from '@angular/core';

import {
  CommonModule
}
from '@angular/common';

import {
  FormsModule
}
from '@angular/forms';

import {
  MatTableModule
}
from '@angular/material/table';

import {
  MatButtonModule
}
from '@angular/material/button';

import {
  MatFormFieldModule
}
from '@angular/material/form-field';

import {
  MatInputModule
}
from '@angular/material/input';

import {
  MatSelectModule
}
from '@angular/material/select';

import {
  AllocationService
}
from '../services/allocation.service';

import {
  EmployeeService
}
from '../services/employee.service';

import {
  ProjectService
}
from '../services/project.service';

import {
  AuthService
}
from '../services/auth.service';

@Component({
  selector: 'app-allocation',

  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule
  ],

  templateUrl:
    './allocation.component.html',

  styleUrls:
    ['./allocation.component.css']
})
export class AllocationComponent
implements OnInit {

  allocationList: any[] = [];

  employees: any[] = [];

  projects: any[] = [];

  displayedColumns = [
    'empId',
    'projectId',
    'assignedOn',
    'createdBy',
    'actions'
  ];

  allocationForm: any = {

    empId: 0,
    projectId: 0,
    assignedOn: '',
    createdBy: 'Admin'
  };

  constructor(

    private allocationService:
      AllocationService,

    private employeeService:
      EmployeeService,

    private projectService:
      ProjectService,

    public authService:
      AuthService

  ) {}

  ngOnInit(): void {

    this.loadAllocations();

    this.loadEmployees();

    this.loadProjects();
  }

  // LOAD ALLOCATIONS

  loadAllocations() {

    this.allocationService
      .getAllocations()
      .subscribe({

        next: (data) => {

          this.allocationList =
            data;
        }
      });
  }

  // LOAD EMPLOYEES

  loadEmployees() {

    this.employeeService
      .getEmployees()
      .subscribe({

        next: (data) => {

          this.employees =
            data;
        }
      });
  }

  // LOAD PROJECTS

  loadProjects() {

    this.projectService
      .getProjects()
      .subscribe({

        next: (data) => {

          this.projects =
            data;
        }
      });
  }

  // ADD

  addAllocation() {

    this.allocationService
      .addAllocation(
        this.allocationForm
      )
      .subscribe({

        next: () => {

          alert(
            'Employee Allocated'
          );

          this.loadAllocations();

          this.resetForm();
        },

        error: (err) => {

          console.log(err);

          alert(
            'Allocation Failed'
          );
        }
      });
  }

  // DELETE

  deleteAllocation(
    id: number
  ) {

    if (
      confirm(
        'Delete Allocation?'
      )
    ) {

      this.allocationService
        .deleteAllocation(id)
        .subscribe({

          next: () => {

            alert(
              'Allocation Deleted'
            );

            this.loadAllocations();
          }
        });
    }
  }

  // EMPLOYEE NAME

  getEmployeeName(
    id: number
  ) {

    const employee =
      this.employees.find(

        e =>
          e.employeeId === id
      );

    return employee

      ? `${employee.firstName}
         ${employee.lastName}`

      : 'Unknown';
  }

  // PROJECT NAME

  getProjectName(
    id: number
  ) {

    const project =
      this.projects.find(

        p =>
          p.projectId === id
      );

    return project

      ? project.projectName

      : 'Unknown';
  }

  // RESET

  resetForm() {

    this.allocationForm = {

      empId: 0,
      projectId: 0,
      assignedOn: '',
      createdBy: 'Admin'
    };
  }
}