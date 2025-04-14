import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import { Stance } from '../../Models/stance';
import { MatDialog } from '@angular/material/dialog';
import { StanceDialogComponent } from '../stance-dialog/stance-dialog.component';
import { SnackbarService } from '../../Services/snackbar.service';
import { ApiserviceService } from '../../Services/apiservice.service';

@Component({
  selector: 'app-mainpage',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './mainpage.component.html',
  styleUrl: './mainpage.component.scss'
})

export class MainpageComponent {
  displayedColumns: string[] = ['name', 'actions'];
  stances: Stance[] = [];

  constructor(
    private service: ApiserviceService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {}

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

  openDialog(data?: Stance): void {
    const dialogRef = this.dialog.open(StanceDialogComponent, {
      width: '300px',
      data: data || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        if (result.originalName) {
          this.service.edditStance(result.originalName, result.newName).subscribe({
            next: () => {
              this.getAllStances();
              this.snackbar.showSuccess('Stanced eddited successfully !');
            },
            error: () => this.snackbar.showError('Failed to edit stance.')
          });
        } else {
          this.service.addStance(result.name).subscribe({
            next: () => {
              this.getAllStances();
              this.snackbar.showSuccess('Stanced added successfully !');
            },
            error: () => this.snackbar.showError('Failed to add stance.')
          });
        }
      }
    });
  }
}