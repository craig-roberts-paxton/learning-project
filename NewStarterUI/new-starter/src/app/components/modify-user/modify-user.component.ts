import { User } from './../../interfaces/user.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { SharedModule } from '../../modules/shared/shared.module';


@Component({
  selector: 'app-modify-user',
  standalone: true,
  imports: [SharedModule],
  templateUrl: './modify-user.component.html',
  styleUrl: './modify-user.component.css'
})

export class ModifyUserComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ModifyUserComponent>, @Inject(MAT_DIALOG_DATA) public data: { header: string, user: User }) { }

  ngOnInit() {
    console.log(this.data);
  }

  onCancel(): void {
    this.dialogRef.close();
  }



}