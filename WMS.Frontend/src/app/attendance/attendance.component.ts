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

import { AttendanceService }
from '../services/attendance.service';

import { EmployeeService }
from '../services/employee.service';

import * as XLSX
from 'xlsx';

import * as FileSaver
from 'file-saver';

@Component({
  selector: 'app-attendance',

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
    './attendance.component.html',

  styleUrls:
    ['./attendance.component.css']
})
export class AttendanceComponent
implements OnInit {

  attendanceList: any[] = [];

  employees: any[] = [];

  displayedColumns = [

    'attendanceId',

    'employeeName',

    'attendanceDate',

    'checkInTime',

    'checkOutTime',

    'workingHours',

    

    'status'
  ];

  employeeId = 0;

  workMode = 'Office';

  isCheckedIn = false;

  isCheckedOut = false;

  selectedMonth = new Date().getMonth() + 1;

  selectedYear = new Date().getFullYear();    

  constructor(

    private attendanceService:
      AttendanceService,

    private employeeService:
      EmployeeService

  ) {}

  ngOnInit(): void {

    this.loadAttendance();

    this.loadEmployees();
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

  loadAttendance() {

    this.attendanceService
      .getAttendance()
      .subscribe({

        next: (data) => {

          this.attendanceList = data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  loadMonthlyAttendance() {

  if (!this.employeeId) {

    alert('Select Employee');

    return;
  }

  this.attendanceService
    .getMonthlyAttendance(

      this.employeeId,

      this.selectedMonth,

      this.selectedYear

    )
    .subscribe({

      next: (data) => {

        this.attendanceList = data;
      },

      error: (err) => {

        console.log(err);

        alert(
          'Failed to Load Monthly Attendance'
        );
      }
    });
}

  checkIn() {

    const payload = {

      employeeId:
        this.employeeId,

      workMode:
        this.workMode
    };

    this.attendanceService
      .checkIn(payload)
      .subscribe({

        next: () => {

          alert(
            'Checked In Successfully'
          );

          this.isCheckedIn = true;

          this.loadAttendance();
        },

        error: (error) => {

  alert(
    error.error.message
    || error.error
    || 'Check In Failed'
  );
}
      });
  }

  checkOut() {

    const payload = {

      employeeId:
        this.employeeId
    };

    this.attendanceService
      .checkOut(payload)
      .subscribe({

        next: () => {

          alert(
            'Checked Out Successfully'
          );

          this.isCheckedOut = true;

          this.loadAttendance();
        },

        error: (error) => {

  alert(
    error.error.message
    || error.error
    || 'Checkout Failed'
  );
}
      });
  }

  exportAttendanceReport() {

  const worksheet =
    XLSX.utils.json_to_sheet(

      this.attendanceList.map(

        item => ({

          Employee:
            this.getEmployeeName(
              item.employeeId
            ),

          Date:
            item.attendanceDate,

          CheckIn:
            item.checkInTime,

          CheckOut:
            item.checkOutTime,

          Hours:
            item.workingHours,

          Status:
            item.status
        })
      )
    );

  const workbook =
    XLSX.utils.book_new();

  XLSX.utils.book_append_sheet(

    workbook,

    worksheet,

    'Attendance'
  );

  const excelBuffer =
    XLSX.write(

      workbook,

      {

        bookType: 'xlsx',

        type: 'array'
      }
    );

  const data =
    new Blob(

      [excelBuffer],

      {

        type:
          'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;charset=UTF-8'
      }
    );

  FileSaver.saveAs(

    data,

    'Attendance_Report.xlsx'
  );
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