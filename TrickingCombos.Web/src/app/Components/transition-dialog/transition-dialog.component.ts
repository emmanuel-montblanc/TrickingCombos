import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import { Stance } from '../../Models/Stance';

@Component({
  selector: 'app-transition-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './transition-dialog.component.html',
  styleUrl: './transition-dialog.component.scss'
})
export class TransitionDialogComponent {
  id: string | null;
  form: FormGroup;
  stances: Stance[] = [];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<TransitionDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.stances = data.stances;
    this.id = data.transition?.id || null;

    const selectedStances = data.transition?.stances?.map((stance: any) => stance.id) || [];
    this.form = this.fb.group({
      name: [data.transition?.name || ''],
      stances: [selectedStances] 
    });
  }

  save() {
    const updatedTranstion = {
      name: this.form.value.name,
      stancesIds: this.form.value.stances,
      id: this.id
    }
    this.dialogRef.close(updatedTranstion);
  }

  cancel() {
    this.dialogRef.close();
  }
}
