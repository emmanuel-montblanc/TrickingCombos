import { Component } from '@angular/core';
import { ApiserviceService } from '../../Services/apiservice.service';
import { SnackbarService } from '../../Services/snackbar.service';
import { MatDialog } from '@angular/material/dialog';
import { Transition } from '../../Models/Transition';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { Stance } from '../../Models/stance';
import { TransitionDialogComponent } from '../transition-dialog/transition-dialog.component';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-transition-page',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './transition-page.component.html',
  styleUrl: './transition-page.component.scss'
})
export class TransitionPageComponent {
  displayedColumns: string[] = ['name', 'stances', 'actions'];
  stances: Stance[] = [];
  transitions: Transition[] = [];

  constructor(
    private service: ApiserviceService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {}

  ngOnInit() {
    this.getAllTransitions();
    this.getAllStances();
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

  getStanceNames(element: any): string {
    return element.stances?.map((s: any) => s.name).join(', ') || '';
  }

  openDialog(data?: Transition): void {
    const dialogRef = this.dialog.open(TransitionDialogComponent, {
      width: '350px',
      data: { 
        stances: this.stances,
        transition: data
       }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        console.log(result);
        if (result.id) {
          this.editTransition(result.id, result.name, result.stancesIds);
        } else {
          this.addTransition(result.name, result.stancesIds);
        }
      }
    });
  }

  addTransition(name: string, stanceIds: string[]) {
    this.service.addTransition(name, stanceIds).subscribe({
      next: () => {
        this.getAllTransitions();
        this.snackbar.showSuccess(`La transition "${name}" a bien été ajoutée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de l'ajout de la transition "${name}"`)
    });
  }

  editTransition(id: string, name: string, stanceIds: string[]) {
    this.service.editTransition(id, name, stanceIds).subscribe({
      next: () => {
        this.getAllTransitions();
        this.snackbar.showSuccess(`La transition "${name}" a bien été modifiée !`);
      },
      error: () => this.snackbar.showError(`Une erreur est survenue lors de la modification de la transition "${name}"`)
    });
  }

  confirmDelete(transition: Transition): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Suppression de transition',
        message: `Etes vous sûrs de vouloir supprimer la transition "${transition.name}" ? Cette suppression est définitive.`
      },
      autoFocus: false
    });
  
    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.deleteTransition(transition)
      }
    });
  }

  deleteTransition(transition: Transition) {
    this.service.deleteTransition(transition.id).subscribe({
      next: () => {
        this.snackbar.showInfo(`La transition "${transition.name}" a bien été supprimée !`);
        this.getAllTransitions();
      },
      error: () => {
        this.snackbar.showError(`Une erreur est survenue lors de la suppression de la transition "${transition.name}"`);
      }
    });
  }
}
