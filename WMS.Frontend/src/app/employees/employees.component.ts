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

import { EmployeeService }
from '../services/employee.service';

@Component({
  selector: 'app-employees',

  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],

  templateUrl:
    './employees.component.html',

  styleUrls:
    ['./employees.component.css']
})
export class EmployeesComponent
implements OnInit {

  employees: any[] = [];

  filteredEmployees: any[] = [];

  searchText = '';

  showForm = false;

  displayedColumns = [
    'employeeId',
    'firstName',
    'lastName',
    'email',
    'phoneNumber',
    'actions'
  ];

  employeeForm: any = {

    firstName: '',

    lastName: '',

    email: '',

    phoneNumber: ''
  };

  isEditMode = false;

  selectedEmployeeId = 0;

  constructor(
    private employeeService:
      EmployeeService
  ) {}

  ngOnInit(): void {

    this.loadEmployees();
  }

  loadEmployees() {

    this.employeeService
      .getEmployees()
      .subscribe({

        next: (data) => {

          this.employees = data;

          this.filteredEmployees = data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  searchEmployees() {

    this.filteredEmployees =
      this.employees.filter(e =>

        e.firstName
          .toLowerCase()
          .includes(
            this.searchText
              .toLowerCase()
          )
      );
  }

  openAddForm() {

    this.showForm = true;

    this.isEditMode = false;

    this.employeeForm = {

      firstName: '',

      lastName: '',

      email: '',

      phoneNumber: ''
    };
  }

  saveEmployee() {

    if(this.isEditMode) {

      this.employeeService
        .updateEmployee(
          this.selectedEmployeeId,
          this.employeeForm
        )
        .subscribe({

          next: () => {

            alert(
              'Employee updated successfully'
            );

            this.closeForm();

            this.loadEmployees();
          }
        });

    } else {

      this.employeeService
        .addEmployee(
          this.employeeForm
        )
        .subscribe({

          next: () => {

            alert(
              'Employee added successfully'
            );

            this.closeForm();

            this.loadEmployees();
          }
        });
    }
  }

  editEmployee(employee: any) {

    this.showForm = true;

    this.isEditMode = true;

    this.selectedEmployeeId =
      employee.employeeId;

    this.employeeForm = {

      firstName:
        employee.firstName,

      lastName:
        employee.lastName,

      email:
        employee.email,

      phoneNumber:
        employee.phoneNumber
    };
  }

  deleteEmployee(id: number) {

    if(confirm(
      'Are you sure you want to delete this employee?'
    )) {

      this.employeeService
        .deleteEmployee(id)
        .subscribe({

          next: () => {

            alert(
              'Employee deleted successfully'
            );

            this.loadEmployees();
          }
        });
    }
  }

  closeForm() {

    this.showForm = false;

    this.employeeForm = {

      firstName: '',

      lastName: '',

      email: '',

      phoneNumber: ''
    };

    this.isEditMode = false;

    this.selectedEmployeeId = 0;
  }
}