import { Component } from '@angular/core';
import { ApiserviceService } from '../../Services/apiservice.service';
import { SnackbarService } from '../../Services/snackbar.service';
import { MatDialog } from '@angular/material/dialog';
import { Variation } from '../../Models/Variation';
import { Transition } from '../../Models/Transition';
import { Stance } from '../../Models/Stance';
import { Trick } from '../../Models/Trick';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';
import { TrickDialogComponent } from '../trick-dialog/trick-dialog.component';

@Component({
  selector: 'app-trick-page',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './trick-page.component.html',
  styleUrl: './trick-page.component.scss'
})

export class TrickPageComponent {
  displayedColumns: string[] = ['name', 'defaultLandingStance', 'transitions', 'variations', 'actions'];
  tricks: Trick[] = [];
  stances: Stance[] = [];
  transitions: Transition[] = [];
  variations: Variation[] = [];

  constructor(
    private service: ApiserviceService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getAllTricks();
    this.getAllTransitions();
    this.getAllVariations();
    this.getAllStances();
  }

  getAllTricks() {
    this.service.getAllTricks().subscribe({
      next: (res: Trick[]) => {
        
        this.tricks = res;
      },
      error: (error: any) => {
        console.error(error);
        this.snackbar.showError(`Une erreur est survenue lors de la récupération des tricks`);
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

  getAllTransitions() {
    this.service.getAllTransitions().subscribe({
      next: (res: Transition[]) => {
        this.transitions = res;
      },
      error: (error: any) => {
        console.error(error);
        this.snackbar.showError(`Une erreur est survenue lors de la récupération des transitions`);
      }
    });
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

  getTransitionNames(trick: Trick): string {
    return trick.transitions?.map((s: any) => s.name).join(', ') || '';
  }
 
  getVariationNames(trick: Trick): string {
    return trick.variations?.map((s: any) => s.name).join(', ') || '';
  }

  openDialog(trick?: Trick): void {
    const dialogRef = this.dialog.open(TrickDialogComponent, {
      width: '350px',
      data: { 
        stances: this.stances,
        transitions: this.transitions,
        variations: this.variations,
        trick: trick
        }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        if (result.id) {
          this.editTrick(result.id, result.name, result.defaultLandingStanceId, result.transitionIds, result.variationIds);
        } else {
          this.addTrick(result.name, result.defaultLandingStanceId, result.transitionIds, result.variationIds);
        }
      }
    });
  }

  addTrick(name: string, defaultLandingstanceId: string, transitionsIds: string[], variationsId: string[]) {
    this.service.addTrick(name, defaultLandingstanceId, transitionsIds, variationsId).subscribe({
      next: () => {
        this.getAllTricks();
        this.snackbar.showSuccess(`Le trick "${name}" a bien été ajoutée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de l'ajout du trick "${name}"`)
    });
  }

  editTrick(id: string, name: string, defaultLandingstanceId: string, transitionsIds: string[], variationsId: string[]) {
    this.service.editTrick(id, name, defaultLandingstanceId, transitionsIds, variationsId).subscribe({
      next: () => {
        this.getAllTricks();
        this.snackbar.showSuccess(`La transition "${name}" a bien été modifiée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de la modification de la transition "${name}"`)
    });
  }

    confirmDelete(trick: Trick): void {
      const dialogRef = this.dialog.open(ConfirmDialogComponent, {
        data: {
          title: 'Suppression de trick',
          message: `Etes vous sûrs de vouloir supprimer le trick "${trick.name}" ? Cette suppression est définitive.`
        },
        autoFocus: false
      });
    
      dialogRef.afterClosed().subscribe(confirmed => {
        if (confirmed) {
          this.deleteTrick(trick)
        }
      });
    }
  
    deleteTrick(trick: Trick) {
      this.service.deleteTrick(trick.id).subscribe({
        next: () => {
          this.snackbar.showInfo(`Le trick "${trick.name}" a bien été supprimé !`);
          this.getAllTricks();
        },
        error: () => {
          this.snackbar.showError(`Une erreur est survenue lors de la suppression du trick "${trick.name}"`);
        }
      });
    }
}
