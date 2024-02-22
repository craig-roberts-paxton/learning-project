import { NgModule } from '@angular/core';
import { CommonModule, NgFor } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatButton } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatDialogClose } from '@angular/material/dialog';
import { MatFormField } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatTable, MatHeaderCell, MatHeaderCellDef, MatHeaderRowDef, MatCell, MatCellDef, MatHeaderRow, MatRow, MatRowDef, MatTableModule } from '@angular/material/table';
import { MatOption, MatSelect } from '@angular/material/select';
import { MatPaginator } from '@angular/material/paginator';



@NgModule({
  declarations: [],
  imports: [MatFormField, MatTable, MatButton, MatFormField, MatDialogClose, MatHeaderCell,
    MatHeaderCellDef, MatHeaderRowDef, MatCell, MatCellDef, MatHeaderRow, MatRow, MatRowDef, MatSelect, MatOption, MatPaginator,
    CommonModule],
  exports: [MatFormField, MatTable, MatButton, MatFormField, FormsModule, MatDialogClose, MatCardModule,
    MatInputModule, MatHeaderCell, MatHeaderCellDef, MatHeaderRowDef, MatCell, MatCellDef,
    MatHeaderRow, MatRow, MatRowDef, ReactiveFormsModule, MatTableModule, MatSelect, MatOption, NgFor, MatPaginator]
})
export class SharedModule { }
