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
        console.log(res);
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
        console.log(res);
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
          // todo
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
}
