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
import { Variation } from '../../Models/Variation';
import { Transition } from '../../Models/Transition';

@Component({
  selector: 'app-trick-dialog',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatDialogModule,
    MatButtonModule,
    MatSelectModule
  ],
  templateUrl: './trick-dialog.component.html',
  styleUrl: './trick-dialog.component.scss'
})

export class TrickDialogComponent {
  id: string | null;
  form: FormGroup;
  stances: Stance[] = [];
  transitions: Transition[] = [];
  variations: Variation[] = [];

  constructor(
    private fb: FormBuilder,
    public dialogRef: MatDialogRef<TrickDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) {
    this.stances = data.stances;
    this.transitions = data.transitions;
    this.variations = data.variations;
    this.id = data.trick?.id || null;

    const transitionIds = data.trick?.transitions?.map((t: Transition) => t.id) || [];
    const variationIds = data.trick?.variations?.map((v: Variation) => v.id) || [];

    this.form = this.fb.group({
      name: [data.trick?.name || ''],
      defaultLandingStanceId: [data.trick?.defaultLandingStance.id || '' ],
      transitionIds: [transitionIds],
      variationIds: [variationIds]
    });
  }

  save() {
    console.log(this.form.value);

    const updatedTrick = {
      id: this.id,
      name: this.form.value.name,
      defaultLandingStanceId: this.form.value.defaultLandingStanceId,
      transitionIds: this.form.value.transitionIds,
      variationIds: this.form.value.variationIds
    }
    this.dialogRef.close(updatedTrick);
  }

  cancel() {
    this.dialogRef.close();
  }
}
