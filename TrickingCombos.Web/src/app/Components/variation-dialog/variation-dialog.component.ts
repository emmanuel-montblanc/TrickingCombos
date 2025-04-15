import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogModule } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import {MatSelectModule} from '@angular/material/select';
import { Stance } from '../../Models/stance';

@Component({
  selector: 'app-variation-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './variation-dialog.component.html',
  styleUrl: './variation-dialog.component.scss'
})

export class VariationDialogComponent {
  id: string | null;
  form: FormGroup;
  stances: Stance[] = [];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<VariationDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    console.log(data);
    this.stances = data.stances;
    this.id = data.variation?.id || null;

    this.form = this.fb.group({
      name: [data.variation?.name || ''],
      landingStanceId: [data.variation?.landingStance.id || '' ] 
    });
  }

  save() {
    const updatedVariation = {
      name: this.form.value.name,
      landingStanceId: this.form.value.landingStanceId,
      id: this.id
    }
    console.log(updatedVariation);
    this.dialogRef.close(updatedVariation);
  }

  cancel() {
    this.dialogRef.close();
  }
}
