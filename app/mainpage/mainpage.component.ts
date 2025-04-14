import { Component, inject } from '@angular/core';
import { MaterialModule } from '../material/material.module';

import { MatSnackBar } from '@angular/material/snack-bar';
import { ServiceService } from '../service.service';
import {MatTableModule} from '@angular/material/table';
import { Stance } from '../Models/stance';


@Component({
  selector: 'app-mainpage',
  imports: [MaterialModule, MatTableModule],
  templateUrl: './mainpage.component.html',
  styleUrl: './mainpage.component.css'
})
export class MainpageComponent {
  private _snackbar = inject(MatSnackBar);
  //this._snackbar.open(message, action);

  stances: Stance[] = [];

  constructor(private service: ServiceService){}

  ngOnInit() {
    this.getAllStances()
  }

  getAllStances() {
    this.service.getAllStances().subscribe({
      next: (res: any) => {
        console.log(res);
        this.stances = res
      },
      error: (error: any) => {
        console.error(error);
      }
    });
  }
}
