import { Component } from '@angular/core';
import { ApiserviceService } from '../../Services/apiservice.service';
import { User } from '../../Models/User';
import { SnackbarService } from '../../Services/snackbar.service';
import { MatTableModule } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from '../confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-admin-page',
  imports: [MatTableModule, MatButtonModule, MatIconModule],
  templateUrl: './admin-page.component.html',
  styleUrl: './admin-page.component.scss'
})
export class AdminPageComponent {
  displayedColumns: string[] = ['id', 'username', 'email', 'role', 'actions'];
  users: User[] = [];

  constructor(
    private service: ApiserviceService,
    private snackbar: SnackbarService,
    private dialog: MatDialog
  ) {
    this.getAllUsers();
  }

  getAllUsers() {
    this.service.getAllUsers().subscribe({
      next: (res: any) => {
        this.users = res;
        console.log(this.users);
      },
      error: (error: any) => {
        console.error(error);
        this.snackbar.showError(`Une erreur est survenue lors de la récupération des utilsateurs`);
      }
    });
  }

  confirmDelete(user: User): void {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Suppression d\'utilisateur',
        message: `Etes vous sûrs de vouloir supprimer l'utilisateur "${user.username}" ? Cette suppression est définitive.`
      },
      autoFocus: false
    });
  
    dialogRef.afterClosed().subscribe(confirmed => {
      if (confirmed) {
        this.deleteUser(user);
      }
    });
  }
  
    deleteUser(user: User) {
      this.service.deleteUser(user.id).subscribe({
        next: () => {
          this.snackbar.showInfo(`L'utilisateur "${user.username}" a bien été supprimé !`);
          this.getAllUsers();
        },
        error: (e) => {
          console.error(e);
          this.snackbar.showError(`Une erreur est survenue lors de la suppression de l'utilisateur "${user.username}"`);
        }
      });
    }
}
