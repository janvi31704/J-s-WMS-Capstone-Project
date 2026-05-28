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
  RoleService
}
from '../services/role.service';

@Component({
  selector: 'app-role',

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
    './role.component.html',

  styleUrls:
    ['./role.component.css']
})
export class RoleComponent
implements OnInit {

  roleList: any[] = [];

  filteredRoles: any[] = [];

  searchText = '';

  displayedColumns = [

    'roleId',

    'roleName',

    'actions'
  ];

  roleForm: any = {

    roleName: ''
  };

  isEditMode = false;

  selectedRoleId = 0;

  constructor(

    private roleService:
      RoleService

  ) {}

  ngOnInit(): void {

    this.loadRoles();
  }

  loadRoles() {

    this.roleService
      .getRoles()
      .subscribe({

        next: (data) => {

          this.roleList = data;

          this.filteredRoles = data;
        }
      });
  }

  searchRoles() {

    this.filteredRoles =
      this.roleList.filter(r =>

        r.roleName
          .toLowerCase()
          .includes(
            this.searchText
              .toLowerCase()
          )
      );
  }

  saveRole() {

    if(
      !this.roleForm.roleName
    ) {

      alert(
        'Enter Role Name'
      );

      return;
    }

    if(this.isEditMode) {

      this.roleService
        .updateRole(

          this.selectedRoleId,

          this.roleForm
        )
        .subscribe({

          next: () => {

            alert(
              'Role Updated'
            );

            this.loadRoles();

            this.resetForm();
          }
        });

    } else {

      this.roleService
        .addRole(
          this.roleForm
        )
        .subscribe({

          next: () => {

            alert(
              'Role Added'
            );

            this.loadRoles();

            this.resetForm();
          }
        });
    }
  }

  editRole(role: any) {

    this.isEditMode = true;

    this.selectedRoleId =
      role.roleId;

    this.roleForm = {

      roleName:
        role.roleName
    };
  }

  deleteRole(id: number) {

    if(confirm(
      'Delete Role?'
    )) {

      this.roleService
        .deleteRole(id)
        .subscribe({

          next: () => {

            alert(
              'Role Deleted'
            );

            this.loadRoles();
          }
        });
    }
  }

  resetForm() {

    this.roleForm = {

      roleName: ''
    };

    this.isEditMode = false;

    this.selectedRoleId = 0;
  }
}