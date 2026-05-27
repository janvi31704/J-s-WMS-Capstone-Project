import { Injectable }
from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LeaveService {

  private apiUrl =
    'http://localhost:5148/api/LeaveRequest';

  constructor(

    private http:
      HttpClient

  ) {}

  // GET ALL LEAVES

  getLeaves():
    Observable<any[]> {

    return this.http.get<any[]>(

      this.apiUrl
    );
  }

  // APPLY LEAVE

  applyLeave(
    leave: any
  ) {

    return this.http.post(

      this.apiUrl,

      leave,
      {

      responseType: 'text'as 'json'
    });
  }

  // APPROVE LEAVE

  approveLeave(
    id: number
  ) {

    return this.http.put(

      `${this.apiUrl}/${id}/approve`,

      {}
    );
  }

  // REJECT LEAVE

  rejectLeave(
    id: number
  ) {

    return this.http.put(

      `${this.apiUrl}/${id}/reject`,

      {}
    );
  }
}