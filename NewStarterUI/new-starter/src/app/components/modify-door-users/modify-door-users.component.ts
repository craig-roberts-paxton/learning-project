import { AccessControlService } from './../../services/access-control.service';
import { SharedModule } from '../../modules/shared/shared.module';
import { User } from './../../interfaces/user.model';
import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { UserService } from '../../services/user.service';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { Door } from '../../interfaces/door.model';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';

@Component({
  selector: 'app-modify-door-user',
  standalone: true,
  imports: [SharedModule,],
  templateUrl: './modify-door-users.component.html',
  styleUrl: './modify-door-users.component.css'
})

export class ModifyDoorUsersComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private accessControlService: AccessControlService, public dialogRef: MatDialogRef<ModifyDoorUsersComponent>,
    @Inject(MAT_DIALOG_DATA) public data: { door: Door }) { }

  // declare an array to store the items

  doorUsers: MatTableDataSource<User> = new MatTableDataSource<User>();
  addableUsers: User[] = [];

  columnsToDisplay: string[] = ['firstName', 'lastName', 'userName', 'action'];


  selectedUserToAdd: User | undefined = undefined;

  // on initialization, call the web api and assign the response to the items array
  ngOnInit() {
    this.getUsersForDoor();
    this.getAddableUsers();
  }


  getUsersForDoor(): void {
    this.accessControlService.getUsersAssignedToDoor(this.data.door.doorId).subscribe({
      next: (data) => { this.doorUsers = new MatTableDataSource(data); this.doorUsers.paginator = this.paginator; },
      error: (error) => console.log("Error fetching door data: ", error)
    })
  }


  removeUserFromDoor(userId: number): void {
    this.accessControlService.removeAccessToDoor(this.data.door.doorId, userId).subscribe(
      {
        next: () => { this.getUsersForDoor(); this.getAddableUsers(); },
        error: (error) => console.log("Error removing user from door")
      }
    )
  }


  getAddableUsers(): void {
    this.accessControlService.getUsersNotAssignedToDoor(this.data.door.doorId).subscribe({
      next: (data) => this.addableUsers = data,
      error: (error) => console.log("Error fetching door data: ", error)
    })
  }

  addUserToDoor(): void {
    if (this.selectedUserToAdd == undefined) {
      return;
    }
    this.accessControlService.allowAccessToDoor(this.data.door.doorId, this.selectedUserToAdd?.userId ?? -1).subscribe({
      next: () => {
        this.getUsersForDoor();
        this.getAddableUsers()
      },
      error: () => console.log("Error adding user to door")
    }
    )
  }



}