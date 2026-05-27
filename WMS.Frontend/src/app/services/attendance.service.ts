import { Injectable }
from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AttendanceService {

  private apiUrl =
    'http://localhost:5148/api/Attendance';

  constructor(
    private http: HttpClient
  ) {}

  getAttendance():
  Observable<any> {

    return this.http.get(
      this.apiUrl
    );
  }

  checkIn(data: any):
  Observable<any> {

    return this.http.post(
      `${this.apiUrl}/checkin`,
      data
    );
  }

  checkOut(data: any):
  Observable<any> {

    return this.http.post(
      `${this.apiUrl}/checkout`,
      data
    );
  }

  getMonthlyAttendance(
  employeeId: number,
  month: number,
  year: number
) {

  return this.http.get<any[]>(

    `${this.apiUrl}/monthly/${employeeId}?month=${month}&year=${year}`

  );
}
}