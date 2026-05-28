import { Injectable }
from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RoleService {

 
  private apiUrl =
'https://janvi-wms-api-hrhta5a5g4fwcrg9.centralindia-01.azurewebsites.net/api/Role';

  constructor(
    private http: HttpClient
  ) {}

  getRoles(): Observable<any[]> {

    return this.http.get<any[]>(
      this.apiUrl
    );
  }

  addRole(role: any) {

    return this.http.post(
      this.apiUrl,
      role
    );
  }

  updateRole(
    id: number,
    role: any
  ) {

    return this.http.put(

      `${this.apiUrl}/${id}`,

      role
    );
  }

  deleteRole(id: number) {

    return this.http.delete(

      `${this.apiUrl}/${id}`
    );
  }
}