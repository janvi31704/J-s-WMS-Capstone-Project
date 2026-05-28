import {
  Component,
  Inject
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
  MAT_DIALOG_DATA,
  MatDialogRef,
  MatDialogModule
}
from '@angular/material/dialog';

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

@Component({
  selector:
    'app-department-dialog',

  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    MatDialogModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule
  ],

  templateUrl:
    './department-dialog.component.html',

  styleUrls:
    ['./department-dialog.component.css']
})
export class
DepartmentDialogComponent {

  constructor(

    public dialogRef:
      MatDialogRef<
        DepartmentDialogComponent
      >,

    @Inject(MAT_DIALOG_DATA)
    public data: any

  ) {}

  save() {

    this.dialogRef.close(
      this.data
    );
  }

  close() {

    this.dialogRef.close();
  }
}