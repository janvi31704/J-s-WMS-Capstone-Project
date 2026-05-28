import { Injectable }
from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DashboardService {

  

  private apiUrl =
'https://janvi-wms-api-hrhta5a5g4fwcrg9.centralindia-01.azurewebsites.net/api/dashboard';

  constructor(
    private http: HttpClient
  ) {}

  getSummary(): Observable<any> {

    return this.http.get(
      `${this.apiUrl}/summary`
    );
  }
}