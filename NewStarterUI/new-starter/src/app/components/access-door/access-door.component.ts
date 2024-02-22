import { UserService } from './../../services/user.service';
import { Component, Inject, OnInit } from '@angular/core';
import { SharedModule } from '../../modules/shared/shared.module';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Door } from '../../interfaces/door.model';
import { AccessControlService } from '../../services/access-control.service';
import { ModifyDoorUsersComponent } from '../modify-door-users/modify-door-users.component';
import { User } from '../../interfaces/user.model';

@Component({
  selector: 'app-access-door',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './access-door.component.html',
  styleUrl: './access-door.component.css'
})
export class AccessDoorComponent implements OnInit {

  constructor(private accessControlService: AccessControlService, private userService: UserService, public dialogRef: MatDialogRef<ModifyDoorUsersComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { door: Door }) { }


  users: User[] = [];

  selectedUser: User = new User();

  doorOpen: boolean | undefined;

  ngOnInit(): void {
    this.getUsers();
  }

  getUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => this.users = data,
      error: (error) => console.log("Problem checking access: ", error)
    });
  }

  checkAccess(): void {
    if (this.selectedUser == undefined)
      return;
    this.accessControlService.validateAccess(this.data.door.doorId, this.selectedUser).subscribe({
      next: () => {
        this.doorOpen = true;
        setTimeout(() => {
          this.doorOpen = undefined;
        }, 5000);
      },
      error: (error) => {
        this.doorOpen = false;
        setTimeout(() => {
          this.doorOpen = undefined;
        }, 5000);
      }
    });
  }
}
