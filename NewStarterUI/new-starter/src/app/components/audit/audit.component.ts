import { AccessControlService } from './../../services/access-control.service';
import { AfterViewInit, Component, OnInit, ViewChild, viewChild } from '@angular/core';
import { SharedModule } from '../../modules/shared/shared.module';
import { DoorService } from '../../services/door.service';
import { UserService } from '../../services/user.service';
import { Door } from '../../interfaces/door.model';
import { User } from '../../interfaces/user.model';
import { AuditRecord } from '../../interfaces/audit.model';
import { MatTableDataSource } from '@angular/material/table';
import { CommonModule } from '@angular/common';
import { MatPaginator } from '@angular/material/paginator';

@Component({
  selector: 'app-audit',
  standalone: true,
  imports: [SharedModule, CommonModule],
  templateUrl: './audit.component.html',
  styleUrl: './audit.component.css'
})


export class AuditComponent implements OnInit {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  constructor(private doorService: DoorService, private userService: UserService, private accessControlService: AccessControlService) { }

  users: User[] = [];
  doors: Door[] = [];
  auditRecords: AuditRecord[] = [];

  dataSource: MatTableDataSource<AuditRecord> = new MatTableDataSource<AuditRecord>();

  columnsToDisplay: string[] = ['doorName', 'userName', 'auditDateTime', 'accessGranted']

  selectedUser: User | undefined = undefined;
  selectedDoor: Door | undefined = undefined;

  ngOnInit() {

    this.getUsers();
    this.getDoors();
    this.getAudits(new Door(), new User());
  }

  getUsers(): void {
    this.userService.getUsers().subscribe({
      next: (data) => this.users = data,
      error: (error) => console.log("Error getting users")
    })
  }

  getDoors(): void {
    this.doorService.getDoors().subscribe({
      next: (data) => this.doors = data,
      error: (error) => console.log("Error getting doors")
    })
  }

  getAudits(door: Door | undefined, user: User | undefined) {

    if (door == undefined) {
      door = new Door();
    }

    if (user == undefined) {
      user = new User();
    }

    this.accessControlService.getAuditRecords(door?.doorId ?? undefined, user?.userId).subscribe({
      next: (data) => {
        this.auditRecords = data;
        this.dataSource = new MatTableDataSource(this.auditRecords);
        this.dataSource.paginator = this.paginator;
      }
    })

  }


}
