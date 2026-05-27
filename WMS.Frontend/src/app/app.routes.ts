import { Routes } from '@angular/router';
import { LoginComponent } from './auth/login/login.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { EmployeesComponent } from './employees/employees.component';
import { AttendanceComponent } from './attendance/attendance.component';
import { LeavesComponent } from './leaves/leaves.component';
import { authGuard }
from './guards/auth.guard';
import { AnnouncementComponent }
from './announcement/announcement.component';
import { LayoutComponent }
from './layout/layout.component';
import { DepartmentComponent }
from './department/department.component';
import { ProjectComponent } from './project/project.component';
import { AllocationComponent } from './allocation/allocation.component';

export const routes: Routes = [

  {
    path: '',

    redirectTo: 'login',

    pathMatch: 'full'
  },

  {
    path: 'login',

    component: LoginComponent
  },

  {
    path: '',

    component: LayoutComponent,

    canActivate: [authGuard],

    children: [

      {
        path: 'dashboard',

        component: DashboardComponent
      },

      {
        path: 'employees',

        component: EmployeesComponent
      },

      {
        path: 'attendance',

        component: AttendanceComponent
      },

      {
        path: 'leaves',

        component: LeavesComponent
      },

      {
        path: 'announcements',

        component:
          AnnouncementComponent
      },
      {
        path: 'departments',

        component: DepartmentComponent
      },
      {
        path: 'projects',

        component: ProjectComponent
      },
      {
        path: 'allocations',

        component: AllocationComponent
      }
    ]
  }
];
