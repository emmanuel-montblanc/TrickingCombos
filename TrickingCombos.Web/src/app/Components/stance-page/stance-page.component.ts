import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import {MatIconModule} from '@angular/material/icon';
import {MatTableModule} from '@angular/material/table';
import { Stance } from '../../Models/Stance';
import { MatDialog } from '@angular/material/dialog';
import { StanceDialogComponent } from '../stance-dialog/stance-dialog.component';
import { SnackbarService } from '../../Services/snackbar.service';
import { ApiserviceService } from '../../Services/apiservice.service';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-stance-page',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './stance-page.component.html',
  styleUrl: './stance-page.component.scss'
})

export class StancePageComponent {
  displayedColumns: string[] = ['name'];
  stances: Stance[] = [];

  constructor(
    private service: ApiserviceService,
    public authService: AuthService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {
    if (authService.isAdmin()) {
      this.displayedColumns.push('actions');
    }
  }

  ngOnInit() {
    this.getAllStances();
  }

  getAllStances() {
    this.service.getAllStances().subscribe({
      next: (res: any) => {
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

  confirmDelete(stance: Stance): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Suppression de stance',
        message: `Etes vous sûrs de vouloir supprimer la stance "${stance.name}" ? Cette suppression est définitive.`
      },
      autoFocus: false
    });
  
    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.deleteStance(stance);
      }
    });
  }

  deleteStance(stance: Stance) {
    this.service.deleteStance(stance.id).subscribe({
      next: () => {
        this.snackbar.showInfo(`La stance "${stance.name}" a bien été supprimée !`);
        this.getAllStances();
      },
      error: () => {
        this.snackbar.showError(`Une erreur est survenue lors de la suppression de la stance "${stance.name}"`);
      }
    });
  }
}