import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule }
from '@angular/common';

import { FormsModule }
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

import { LeaveService }
from '../services/leave.service';

import { EmployeeService }
from '../services/employee.service';

import { AuthService }
from '../services/auth.service';

@Component({
  selector: 'app-leaves',

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
    './leaves.component.html',

  styleUrls:
    ['./leaves.component.css']
})
export class LeavesComponent
implements OnInit {

  leaveList: any[] = [];

  employees: any[] = [];

  displayedColumns: string[] = [];

  leaveForm = {

    employeeId: 0,

    leaveType: '',

    startDate: '',

    endDate: '',

    reason: ''
  };

  constructor(

    private leaveService:
      LeaveService,

    private employeeService:
      EmployeeService ,
    public authService:
      AuthService

  ) {}

  ngOnInit(): void {

    this.loadLeaves();

    this.loadEmployees();

    if(
  this.isManagerOrAdmin()
) {

  this.displayedColumns = [

    'employee',

    'leaveType',

    'startDate',

    'endDate',

    'reason',

    'status',

    'actions'
  ];

} else {

  this.displayedColumns = [

    'employee',

    'leaveType',

    'startDate',

    'endDate',

    'reason',

    'status'
  ];
}
  }

  loadEmployees() {

    this.employeeService
      .getEmployees()
      .subscribe({

        next: (data) => {

          this.employees = data;
        }
      });
  }

  loadLeaves() {

    this.leaveService
      .getLeaves()
      .subscribe({

        next: (data) => {

          this.leaveList = data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  isManagerOrAdmin(): boolean {

  const role =
    this.authService
      .getUserRole();

  return role === 'Admin'
    || role === 'Manager';
}

  applyLeave() {

  if (
    !this.leaveForm.employeeId
  ) {

    alert(
      'Select Employee'
    );

    return;
  }

  if (
    !this.leaveForm.leaveType
  ) {

    alert(
      'Select Leave Type'
    );

    return;
  }

  this.leaveService
    .applyLeave(this.leaveForm)
    .subscribe({

      next: () => {

        alert(
          'Leave Applied Successfully'
        );

        this.loadLeaves();

        this.resetForm();
      },

      error: (err) => {

  console.log(
    'FULL ERROR:',
    err
  );

  console.log(
    'ERROR BODY:',
    err.error
  );

  console.log(
    'STATUS:',
    err.status
  );

  console.log(
    'FORM:',
    this.leaveForm
  );

  alert(
    JSON.stringify(
      err.error
    )
  );
}
    });
}

  approveLeave(id: number) {

    this.leaveService
      .approveLeave(id)
      .subscribe({

        next: () => {

          alert(
            'Leave Approved'
          );

          this.loadLeaves();
        }
      });
  }

  rejectLeave(id: number) {

    this.leaveService
      .rejectLeave(id)
      .subscribe({

        next: () => {

          alert(
            'Leave Rejected'
          );

          this.loadLeaves();
        }
      });
  }

  resetForm() {

    this.leaveForm = {

      employeeId: 0,

      leaveType: '',

      startDate: '',

      endDate: '',

      reason: ''
    };
  }

  getEmployeeName(
    employeeId: number
  ) {

    const employee =
      this.employees.find(

        e =>
          e.employeeId ===
          employeeId
      );

    return employee

      ? employee.firstName
        + ' ' +
        employee.lastName

      : 'Unknown';
  }
}