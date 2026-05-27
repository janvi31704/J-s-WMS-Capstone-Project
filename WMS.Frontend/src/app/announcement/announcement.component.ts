import {
  Component,
  OnInit
} from '@angular/core';

import { CommonModule }
from '@angular/common';

import { FormsModule }
from '@angular/forms';

import {
  MatButtonModule
}
from '@angular/material/button';

import {
  MatFormFieldModule
}
from '@angular/material/form-field';

import {
  MatInputModule
}
from '@angular/material/input';

import {
  MatCardModule
}
from '@angular/material/card';

import {
  MatSelectModule
}
from '@angular/material/select';

import { AnnouncementService }
from '../services/announcement.service';

import { AuthService }
from '../services/auth.service';

@Component({
  selector: 'app-announcement',

  standalone: true,

  imports: [
    CommonModule,
    FormsModule,
    MatButtonModule,
    MatFormFieldModule,
    MatInputModule,
    MatCardModule,
    MatSelectModule
  ],

  templateUrl:
    './announcement.component.html',

  styleUrls:
    ['./announcement.component.css']
})
export class AnnouncementComponent
implements OnInit {

  announcements: any[] = [];

  isEditMode = false;

  editId = 0;

  announcementForm = {

    title: '',

    message: '',

    priority: 'Normal'
  };

  constructor(

    private announcementService:
      AnnouncementService,
    public authService:
      AuthService

  ) {}

  ngOnInit(): void {

    this.loadAnnouncements();
  }

  loadAnnouncements() {

    this.announcementService
      .getAnnouncements()
      .subscribe({

        next: (data) => {

          this.announcements = data;
        },

        error: (err) => {

          console.log(err);
        }
      });
  }

  saveAnnouncement() {

    if (this.isEditMode) {

      this.updateAnnouncement();

      return;
    }

    this.announcementService
      .createAnnouncement(
        this.announcementForm
      )
      .subscribe({

        next: () => {

          alert(
            'Announcement Created'
          );

          this.loadAnnouncements();

          this.resetForm();
        },

        error: (err) => {

          console.log(err);

          alert(
            'Failed to Create Announcement'
          );
        }
      });
  }

  updateAnnouncement() {

    this.announcementService
      .updateAnnouncement(

        this.editId,

        this.announcementForm
      )
      .subscribe({

        next: () => {

          alert(
            'Announcement Updated'
          );

          this.loadAnnouncements();

          this.resetForm();
        }
      });
  }

  editAnnouncement(
    item: any
  ) {

    this.isEditMode = true;

    this.editId =
      item.announcementId;

    this.announcementForm = {

      title:
        item.title,

      message:
        item.message,

      priority:
        item.priority
    };
  }

  deleteAnnouncement(
    id: number
  ) {

    if (
      !confirm(
        'Delete Announcement?'
      )
    ) {

      return;
    }

    this.announcementService
      .deleteAnnouncement(id)
      .subscribe({

        next: () => {

          alert(
            'Announcement Deleted'
          );

          this.loadAnnouncements();
        }
      });
  }

  resetForm() {

    this.isEditMode = false;

    this.editId = 0;

    this.announcementForm = {

      title: '',

      message: '',

      priority: 'Normal'
    };
  }
}