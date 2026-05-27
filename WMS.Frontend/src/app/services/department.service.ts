import { Injectable }
from '@angular/core';

import {
  HttpClient
}
from '@angular/common/http';

import {
  Observable
}
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  private apiUrl =
    'http://localhost:5148/api/Department';

  constructor(
    private http: HttpClient
  ) {}

  // GET ALL

  getDepartments():
  Observable<any[]> {

    return this.http.get<any[]>(
      this.apiUrl
    );
  }

  // ADD

  addDepartment(
    department: any
  ): Observable<any> {

    return this.http.post(
      this.apiUrl,
      department
    );
  }

  // UPDATE

  updateDepartment(
    id: number,
    department: any
  ): Observable<any> {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      department
    );
  }

  // DELETE

  deleteDepartment(
    id: number
  ): Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}