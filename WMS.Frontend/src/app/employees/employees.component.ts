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

import {
  MatSelectModule
}
from '@angular/material/select';

import { DepartmentService } from '../services/department.service';

import { RoleService } from '../services/role.service';

import {
  MatSnackBar,
  MatSnackBarModule
}
from '@angular/material/snack-bar';

@Component({
  selector: 'app-employees',

  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    MatTableModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatSnackBarModule
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

  phoneNumber: '',

  gender: '',

  dob: '',

  doj: '',

  departmentId: '',

  roleId: '',

  status: 'Active'
};

  isEditMode = false;

  selectedEmployeeId = 0;

  departments: any[] = [];

roles: any[] = [];

  constructor(
    private employeeService:
      EmployeeService,
    private departmentService: DepartmentService,
    private roleService: RoleService,
    private snackBar: MatSnackBar
  ) {}

  ngOnInit(): void {

    this.loadEmployees();
    this.loadDepartments();
    this.loadRoles();
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

loadDepartments() {

  this.departmentService
    .getDepartments()
    .subscribe({

      next: (data) => {

        this.departments = data;
      }
    });
}

loadRoles() {

  this.roleService
    .getRoles()
    .subscribe({

      next: (data) => {

        this.roles = data;
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

      phoneNumber: '',

      gender: '',

      dob: '',

      doj: '',

      departmentId: '',

      roleId: '',

    status: 'Active'
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

            this.snackBar.open(
              'Employee updated successfully',
              'Close',
              {
                duration: 3000
              }
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

            this.snackBar.open(
              'Employee added successfully',
              'Close',
              {
                duration: 3000
              }
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

  firstName: employee.firstName,

  lastName: employee.lastName,

  email: employee.email,

  phoneNumber: employee.phoneNumber,

  gender: employee.gender,

  dob: employee.dob,

  doj: employee.doj,

  departmentId: employee.departmentId,

  roleId: employee.roleId,

  status: employee.status
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

            this.snackBar.open(
              'Employee deleted successfully',
              'Close',
              {
                duration: 3000
              }
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

      phoneNumber: '',

      gender: '',

      dob: '',

      doj: '',

      departmentId: '',

      roleId: '',

    status: 'Active'
    };

    this.isEditMode = false;

    this.selectedEmployeeId = 0;
  }
}