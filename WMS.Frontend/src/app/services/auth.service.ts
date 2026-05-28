import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  

  private apiUrl =
'https://janvi-wms-api-hrhta5a5g4fwcrg9.centralindia-01.azurewebsites.net/api/auth';

  private loggedIn =
    new BehaviorSubject<boolean>(
      !!localStorage.getItem('token')
    );

  isLoggedIn$ =
    this.loggedIn.asObservable();

  constructor(
    private http: HttpClient,
    private router: Router
  ) {}

  login(data: any): Observable<any> {

    return this.http.post(
      `${this.apiUrl}/login`,
      data
    );
  }

  saveToken(token: string) {

    localStorage.setItem('token', token);

    this.loggedIn.next(true);
  }

  getToken(): string | null {

    return localStorage.getItem('token');
  }

    // GET ROLE

  getRole(): string {

    const token =
      this.getToken();

    if (!token) {

      return '';
    }

    const payload =
      JSON.parse(

        atob(
          token.split('.')[1]
        )
      );

    return payload[
      'http://schemas.microsoft.com/ws/2008/06/identity/claims/role'
    ];
  }

  // ROLE CHECKS

  isAdmin(): boolean {

    return this.getRole()
      === 'Admin';
  }

  isManager(): boolean {

    return this.getRole()
      === 'Manager';
  }

  isEmployee(): boolean {

    return this.getRole()
      === 'Employee';
  }

  logout() {

    localStorage.removeItem('token');

    this.loggedIn.next(false);

    this.router.navigate(['/login']);
  }

  isAuthenticated(): boolean {

    return !!localStorage.getItem('token');
  }

  getUserRole(): string {

  return localStorage
    .getItem('role') || '';
}
}