import { Door } from './../../interfaces/door.model';
import { Component, Inject, OnInit } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA, MatDialogClose } from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatButton } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';


@Component({
  selector: 'app-modify-door',
  standalone: true,
  imports: [MatFormField, MatButton, MatFormField, FormsModule, MatDialogClose, MatCardModule, MatInputModule],
  templateUrl: './modify-door.component.html',
  styleUrl: './modify-door.component.css'
})

export class ModifyDoorComponent implements OnInit {

  constructor(public dialogRef: MatDialogRef<ModifyDoorComponent>, @Inject(MAT_DIALOG_DATA) public data: { header: string, door: Door }) { }

  ngOnInit() {
    console.log(this.data);
  }

  onCancel(): void {
    this.dialogRef.close();
  }



}
