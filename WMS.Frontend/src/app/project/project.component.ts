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
  ProjectService
}
from '../services/project.service';

import {
  AuthService
}
from '../services/auth.service';

@Component({
  selector: 'app-project',

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
    './project.component.html',

  styleUrls:
    ['./project.component.css']
})
export class ProjectComponent
implements OnInit {

  projectList: any[] = [];

  displayedColumns = [
    'projectId',
    'projectName',
    'clientId',
    'startDate',
    'endDate',
    'status',
    'actions'
  ];

  projectForm: any = {

    projectName: '',
    clientId: 0,
    startDate: '',
    endDate: '',
    status: ''
  };

  isEditMode = false;

  selectedProjectId = 0;

  constructor(

    private projectService:
      ProjectService,

    public authService:
      AuthService

  ) {}

  ngOnInit(): void {

    this.loadProjects();
  }

  // LOAD

  loadProjects() {

    this.projectService
      .getProjects()
      .subscribe({

        next: (data) => {

          this.projectList =
            data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  // SAVE

  saveProject() {

    if (
      !this.projectForm.projectName
    ) {

      alert(
        'Enter Project Name'
      );

      return;
    }

    // UPDATE

    if (
      this.isEditMode
    ) {

      this.projectService
        .updateProject(

          this.selectedProjectId,

          this.projectForm
        )
        .subscribe({

          next: () => {

            alert(
              'Project Updated'
            );

            this.loadProjects();

            this.resetForm();
          }
        });
    }

    // ADD

    else {

      this.projectService
        .addProject(
          this.projectForm
        )
        .subscribe({

          next: () => {

            alert(
              'Project Added'
            );

            this.loadProjects();

            this.resetForm();
          }
        });
    }
  }

  // EDIT

  editProject(
    project: any
  ) {

    this.isEditMode = true;

    this.selectedProjectId =
      project.projectId;

    this.projectForm = {

      projectName:
        project.projectName,

      clientId:
        project.clientId,

      startDate:
        project.startDate
          ?.split('T')[0],

      endDate:
        project.endDate
          ?.split('T')[0],

      status:
        project.status
    };
  }

  // DELETE

  deleteProject(
    id: number
  ) {

    if (
      confirm(
        'Delete Project?'
      )
    ) {

      this.projectService
        .deleteProject(id)
        .subscribe({

          next: () => {

            alert(
              'Project Deleted'
            );

            this.loadProjects();
          }
        });
    }
  }

  // RESET

  resetForm() {

    this.projectForm = {

      projectName: '',
      clientId: 0,
      startDate: '',
      endDate: '',
      status: ''
    };

    this.isEditMode = false;

    this.selectedProjectId = 0;
  }
}