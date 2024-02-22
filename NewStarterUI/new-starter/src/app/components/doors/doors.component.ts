import { Component, Input, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog'
import { Door } from '../../interfaces/door.model';
import { DoorService } from '../../services/door.service';
import { FormControl, FormGroup } from '@angular/forms'
import { ModifyDoorComponent } from '../modify-door/modify-door.component';
import { ModifyDoorUsersComponent } from '../modify-door-users/modify-door-users.component';
import { SharedModule } from '../../modules/shared/shared.module';
import { AccessDoorComponent } from '../access-door/access-door.component';

@Component({
  selector: 'app-root',
  standalone: true,
  templateUrl: './doors.component.html',
  styleUrls: ['./doors.component.css'],
  imports: [SharedModule]

})

export class DoorsComponent implements OnInit {

  // inject the http client service
  constructor(public dialog: MatDialog, private doorService: DoorService) { }

  newDoorForm = new FormGroup(
    {
      doorName: new FormControl('')
    }
  );


  // declare an array to store the items
  doors: Door[] = [];



  columnsToDisplay: string[] = ['doorId', 'doorName', 'action'];

  // on initialization, call the web api and assign the response to the items array
  ngOnInit() {
    this.getDoors()
  }


  getDoors(): void {
    this.doorService.getDoors().subscribe({
      next: (data) => {
        this.doors = data;
      },
      error: (error) => {
        console.log("Error fetching door data: ", error)
      }
    })
  }


  createNewDoor() {
    console.log("Adding door");
    let door = new Door();
    door.doorId = -1;
    door.doorName = this.newDoorForm.value.doorName ?? '';
    this.doorService.addDoor(door).subscribe(() => {
      this.newDoorForm.reset();
      this.getDoors()
    });
  }


  deleteDoor(doorId: number) {
    this.doorService.deleteDoor(doorId).subscribe(() => this.getDoors());
  }


  openDoorDialog(header: string, door: any): void {
    let dialogRef = this.dialog.open(ModifyDoorComponent, {
      width: '250px',

      data: { header: header, door: door }
    });


    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (result !== 'no') {
          // Handle form submission
          console.log(result);
          this.doorService.editDoor(result.door).subscribe(() => this.getDoors())
        } else if (result === 'no') {
          console.log('User clicked no.');
        }
      }
    });
  }

  openUsersDialog(door: Door): void {

    let dialogRef = this.dialog.open(ModifyDoorUsersComponent, {
      width: '500px',

      data: { door: door }
    });



    dialogRef.afterClosed().subscribe(result => {
      if (result !== undefined) {
        if (result !== 'no') {
          // Handle form submission
          console.log(result);
          this.doorService.editDoor(result.door).subscribe(() => this.getDoors())
        } else if (result === 'no') {
          console.log('User clicked no.');
        }
      }
    });
  }

  openAccessDialog(door: Door): void {
    let dialogRef = this.dialog.open(AccessDoorComponent, {
      width: '300px',
      data: { door: door }
    });
  }

}


