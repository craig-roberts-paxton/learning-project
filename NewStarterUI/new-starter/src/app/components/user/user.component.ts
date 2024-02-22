import { SharedModule } from '../../modules/shared/shared.module';
import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog'
import { User } from '../../interfaces/user.model';
import { UserService } from '../../services/user.service';
import { ModifyUserComponent } from '../modify-user/modify-user.component';


@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './user.component.html',
  styleUrls: ['./user.component.css'],
  imports: [SharedModule]

})

export class UserComponent implements OnInit {

  // inject the http client service
  constructor(public dialog: MatDialog, private userService: UserService) { }

  // declare an array to store the items
  users: User[] = [];
  columnsToDisplay: string[] = ['userId', 'firstName', 'lastName', 'userName', 'pinCode', 'action'];

  // on initialization, call the web api and assign the response to the items array
  ngOnInit() {
    this.getUsers()
  }


  getUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => {
        this.users = data;
      },
      error: (error) => {
        console.log("Error fetching door data: ", error)
      }
    })
  }


  deleteUser(doorId: number) {
    this.userService.deleteUser(doorId).subscribe(() => this.getUsers());
  }

  newUser(): void {
    let user = new User();
    this.openDialog("Create User", user);
  }

  openDialog(header: string, user: User): void {
    let dialogRef = this.dialog.open(ModifyUserComponent, {
      width: '250px',

      data: { header: header, user: user }
    });


    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (result !== 'no') {
          // Handle form submission
          console.log(result);
          this.userService.editUser(result.user).subscribe(() => this.getUsers())
        } else if (result === 'no') {
          console.log('User clicked no.');
        }
      }
    });
  }

}


