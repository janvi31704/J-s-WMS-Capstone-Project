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
  DepartmentService
}
from '../services/department.service';

import {
  AuthService
}
from '../services/auth.service';

@Component({
  selector: 'app-department',

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
    './department.component.html',

  styleUrls:
    ['./department.component.css']
})
export class DepartmentComponent
implements OnInit {

  departmentList: any[] = [];

  displayedColumns = [
    'departmentId',
    'departmentName',
    'actions'
  ];

  departmentForm: any = {

    departmentName: ''
  };

  isEditMode = false;

  selectedDepartmentId = 0;

  constructor(

    private departmentService:
      DepartmentService,

    public authService:
      AuthService

  ) {}

  ngOnInit(): void {

    this.loadDepartments();
  }

  // LOAD

  loadDepartments() {

    this.departmentService
      .getDepartments()
      .subscribe({

        next: (data) => {

          this.departmentList =
            data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  // SAVE

  saveDepartment() {

    if (
      !this.departmentForm
        .departmentName
    ) {

      alert(
        'Enter Department Name'
      );

      return;
    }

    // UPDATE

    if (
      this.isEditMode
    ) {

      this.departmentService
        .updateDepartment(

          this.selectedDepartmentId,

          this.departmentForm
        )
        .subscribe({

          next: () => {

            alert(
              'Department Updated'
            );

            this.loadDepartments();

            this.resetForm();
          }
        });

    }

    // ADD

    else {

      this.departmentService
        .addDepartment(
          this.departmentForm
        )
        .subscribe({

          next: () => {

            alert(
              'Department Added'
            );

            this.loadDepartments();

            this.resetForm();
          }
        });
    }
  }

  // EDIT

  editDepartment(
    department: any
  ) {

    this.isEditMode = true;

    this.selectedDepartmentId =
      department.departmentId;

    this.departmentForm = {

      departmentName:
        department.departmentName
    };
  }

  // DELETE

  deleteDepartment(
    id: number
  ) {

    if (
      confirm(
        'Delete Department?'
      )
    ) {

      this.departmentService
        .deleteDepartment(id)
        .subscribe({

          next: () => {

            alert(
              'Department Deleted'
            );

            this.loadDepartments();
          }
        });
    }
  }

  // RESET

  resetForm() {

    this.departmentForm = {

      departmentName: ''
    };

    this.isEditMode = false;

    this.selectedDepartmentId = 0;
  }
}