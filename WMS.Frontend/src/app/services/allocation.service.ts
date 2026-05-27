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
export class AllocationService {

  private apiUrl =
    'http://localhost:5148/api/EmployeeProjectAllocation';

  constructor(
    private http: HttpClient
  ) {}

  // GET ALL

  getAllocations():
  Observable<any[]> {

    return this.http.get<any[]>(
      this.apiUrl
    );
  }

  // ADD

  addAllocation(
    allocation: any
  ): Observable<any> {

    return this.http.post(
      this.apiUrl,
      allocation
    );
  }

  // DELETE

  deleteAllocation(
    id: number
  ): Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}