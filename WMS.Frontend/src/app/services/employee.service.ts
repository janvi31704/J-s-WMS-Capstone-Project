import { Injectable }
from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  private apiUrl =
'https://janvi-wms-api-hrhta5a5g4fwcrg9.centralindia-01.azurewebsites.net/api/Employee';

  constructor(
    private http: HttpClient
  ) {}

  getEmployees():
  Observable<any> {

    return this.http.get(this.apiUrl);
  }

  getEmployeeById(id: number):
  Observable<any> {

    return this.http.get(
      `${this.apiUrl}/${id}`
    );
  }

  addEmployee(employee: any):
  Observable<any> {

    return this.http.post(
      this.apiUrl,
      employee
    );
  }

  updateEmployee(
    id: number,
    employee: any
  ): Observable<any> {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      employee
    );
  }

  deleteEmployee(id: number):
  Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}