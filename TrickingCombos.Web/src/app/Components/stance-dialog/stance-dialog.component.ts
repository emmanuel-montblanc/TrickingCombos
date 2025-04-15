import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { Stance } from '../../Models/stance';

@Component({
  selector: 'app-stance-dialog',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatButtonModule
  ],
  templateUrl: './stance-dialog.component.html',
  styleUrls: ['./stance-dialog.component.scss']
})
export class StanceDialogComponent {
  id: string | null;
  form: FormGroup;

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<StanceDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: Stance | null
  ) {
    this.id = data?.id || null;
    this.form = this.fb.group({
      name: [data?.name || '']
    });
  }

  save() {
    const updatedData = {
      name: this.form.value.name,
      id: this.id
    }
    this.dialogRef.close(updatedData);
  }

  cancel() {
    this.dialogRef.close();
  }
}