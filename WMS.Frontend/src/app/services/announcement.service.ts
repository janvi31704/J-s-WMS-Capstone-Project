import { Injectable }
from '@angular/core';

import { HttpClient }
from '@angular/common/http';

import { Observable }
from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AnnouncementService {

  

  private apiUrl =
'https://janvi-wms-api-hrhta5a5g4fwcrg9.centralindia-01.azurewebsites.net/api/Announcement';

  constructor(

    private http:
      HttpClient

  ) {}

  // GET ALL

  getAnnouncements():
    Observable<any[]> {

    return this.http.get<any[]>(

      this.apiUrl
    );
  }

  // CREATE

  createAnnouncement(
    announcement: any
  ) {

    return this.http.post(

      this.apiUrl,

      announcement,

      {

        responseType:
          'text' as 'json'
      }
    );
  }

  // UPDATE

  updateAnnouncement(
    id: number,

    announcement: any
  ) {

    return this.http.put(

      `${this.apiUrl}/${id}`,

      announcement,

      {

        responseType:
          'text' as 'json'
      }
    );
  }

  // DELETE

  deleteAnnouncement(
    id: number
  ) {

    return this.http.delete(

      `${this.apiUrl}/${id}`,

      {

        responseType:
          'text' as 'json'
      }
    );
  }
}