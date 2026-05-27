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
export class ProjectService {

  private apiUrl =
    'http://localhost:5148/api/Project';

  constructor(
    private http: HttpClient
  ) {}

  // GET ALL

  getProjects():
  Observable<any[]> {

    return this.http.get<any[]>(
      this.apiUrl
    );
  }

  // ADD

  addProject(
    project: any
  ): Observable<any> {

    return this.http.post(
      this.apiUrl,
      project
    );
  }

  // UPDATE

  updateProject(
    id: number,
    project: any
  ): Observable<any> {

    return this.http.put(
      `${this.apiUrl}/${id}`,
      project
    );
  }

  // DELETE

  deleteProject(
    id: number
  ): Observable<any> {

    return this.http.delete(
      `${this.apiUrl}/${id}`
    );
  }
}