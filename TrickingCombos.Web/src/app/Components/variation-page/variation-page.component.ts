import { Component } from '@angular/core';
import { Stance } from '../../Models/Stance';
import { ApiserviceService } from '../../Services/apiservice.service';
import { SnackbarService } from '../../Services/snackbar.service';
import { MatDialog } from '@angular/material/dialog';
import { Variation } from '../../Models/Variation';
import { VariationDialogComponent } from '../variation-dialog/variation-dialog.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../../Services/auth.service';

@Component({
  selector: 'app-variation-page',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './variation-page.component.html',
  styleUrl: './variation-page.component.scss'
})
export class VariationPageComponent {
  displayedColumns: string[] = ['name', 'stances'];
  stances: Stance[] = [];
  variations: Variation[] = [];

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
    this.getAllVariations();
    this.getAllStances();
  }

  getAllVariations() {
    this.service.getAllVariations().subscribe({
      next: (res: any) => {
        this.variations = res;
      },
      error: (error: any) => {
        console.error(error);
        this.snackbar.showError(`Une erreur est survenue lors de la récupération des variations`);
      }
    });
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

  openDialog(variation?: Variation): void {
    const dialogRef = this.dialog.open(VariationDialogComponent, {
      width: '350px',
      data: { 
        stances: this.stances,
        variation: variation
        }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        if (result.id) {
          this.editVariation(result.id, result.name, result.landingStanceId);
        } else {
          this.addVariation(result.name, result.landingStanceId);
        }
      }
    });
  }

  addVariation(name: string, landingStanceId: string) {
    this.service.addVariation(name, landingStanceId).subscribe({
      next: () => {
        this.getAllVariations();
        this.snackbar.showSuccess(`La variation "${name}" a bien été ajoutée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de l'ajout de la variation "${name}"`)
    });
  }
  
  editVariation(id: string, name: string, landingStanceId: string) {
    this.service.editVariation(id, name, landingStanceId).subscribe({
      next: () => {
        this.getAllVariations();
        this.snackbar.showSuccess(`La variation "${name}" a bien été modifiée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de la modification de la variation "${name}"`)
    });
  }
  
  confirmDelete(variation: Variation): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Suppression de variations',
        message: `Etes vous sûrs de vouloir supprimer la variation "${variation.name}" ? Cette suppression est définitive.`
      },
      autoFocus: false
    });
  
    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.deleteVariation(variation)
      }
    });
  }
  
  deleteVariation(variation: Variation) {
    this.service.deleteVariation(variation.id).subscribe({
      next: () => {
        this.snackbar.showInfo(`La variation "${variation.name}" a bien été supprimée !`);
        this.getAllVariations();
      },
      error: () => {
        this.snackbar.showError(`Une erreur est survenue lors de la suppression de la variation "${variation.name}"`);
      }
    });
  }
}
