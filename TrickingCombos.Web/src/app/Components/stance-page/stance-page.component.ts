import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import { Stance } from '../../Models/stance';
import { MatDialog } from '@angular/material/dialog';
import { StanceDialogComponent } from '../stance-dialog/stance-dialog.component';
import { SnackbarService } from '../../Services/snackbar.service';
import { ApiserviceService } from '../../Services/apiservice.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-stance-page',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './stance-page.component.html',
  styleUrl: './stance-page.component.scss'
})

export class StancePageComponent {
  displayedColumns: string[] = ['name', 'actions'];
  stances: Stance[] = [];

  constructor(
    private service: ApiserviceService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getAllStances();
  }

  getAllStances() {
    this.service.getAllStances().subscribe({
      next: (res: any) => {
        console.log(res);
        this.stances = res
      },
      error: (error: any) => {
        console.error(error);
        this.snackbar.showError(`Une erreur est survenue lors de la récupération des stances`);
      }
    });
  }

  openDialog(data?: Stance): void {
    const dialogRef = this.dialog.open(StanceDialogComponent, {
      width: '350px',
      data: data || null
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        if (result.id) {
          this.editStance(result);
        } else {
          this.addStance(result.name);
        }
      }
    });
  }

  editStance(stance: Stance) {
    this.service.edditStance(stance.id, stance.name).subscribe({
      next: () => {
        this.getAllStances();
        this.snackbar.showSuccess(`La stance "${stance.name}" a bien été modifiée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de la modification de la stance "${stance.name}"`)
    });
  }

  addStance(name: string) {
    this.service.addStance(name).subscribe({
      next: () => {
        this.getAllStances();
        this.snackbar.showSuccess(`La stance "${name}" a bien été ajoutée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de l'ajout de la stance "${name}"`)
    });
  }

  confirmDelete(data: Stance): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Suppression de stance',
        message: `Etes vous sûrs de vouloir supprimer la stance "${data.name}" ? Cette suppression est définitive.`
      },
      autoFocus: false
    });
  
    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.service.deleteStance(data.id).subscribe({
          next: () => {
            this.snackbar.showInfo(`La stance "${data.name}" a bien été supprimée !`);
            this.getAllStances();
          },
          error: () => {
            this.snackbar.showError(`Une erreur est survenue lors de la suppression de la stance "${data.name}"`);
          }
        });
      }
    });
  }
}