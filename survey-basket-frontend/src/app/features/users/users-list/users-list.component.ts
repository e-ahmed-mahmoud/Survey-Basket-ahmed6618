import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatChipsModule } from '@angular/material/chips';
import { MatDialog, MatDialogModule } from '@angular/material/dialog';
import { UsersService } from '../../../core/services/users.service';
import { UserListItem } from "../../../shared/models/UserListItem";
import { UserAccountForm } from '../../../shared/components/user-account-form/user-account-form';
import { ConfirmDialogComponent } from '../../../shared/components/confirm-dialog/confirm-dialog.component';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-users-list',
  standalone: true,
  imports: [
    CommonModule, MatTableModule, MatButtonModule,
    MatIconModule, MatProgressBarModule, MatChipsModule, MatDialogModule,
  ],
  templateUrl: './users-list.component.html',
})
export class UsersListComponent implements OnInit {
  private readonly usersService = inject(UsersService);
  private readonly snackbarService = inject(MatSnackBar);

  private readonly dialog = inject(MatDialog);


  loading = signal(true);
  dataSource = new MatTableDataSource<UserListItem>([]);
  cols = ['name', 'email', 'roles', 'status', 'actions'];

  ngOnInit(): void {
    this.loadUsers()
  }

  loadUsers() {
    this.usersService.getAll().subscribe({
      next: (users) => { this.dataSource.data = users; this.loading.set(false); console.log("user loaded"); },
      error: () => this.loading.set(false),
    });
  }

  onAddUser(): void {
    const ref = this.dialog.open(UserAccountForm, {
      width: '600px',
      data: {},
    });
    ref.afterClosed().subscribe((saved) => {
      if (saved) this.loadUsers();
    });
  }

  onEditUser(u: any): void {
    const user = { firstName: u.firstName, lastName: u.lastName, roles: u.roles, phoneNumber: u.phoneNumber }
    console.log(u);
    const ref = this.dialog.open(UserAccountForm, {
      width: '600px',
      data: { user, isEdit: true, id: u.id },
    });
    ref.afterClosed().subscribe((saved) => {
      if (saved) this.loadUsers();
    });
  }

  onDeleteUser(u: any) {
    const ref = this.dialog.open(ConfirmDialogComponent, {
      data: {
        title: 'Delete Poll',
        message: `Are you sure you want to delete "${u.email}"? This action cannot be undone.`,
        confirmText: 'Delete',
        confirmColor: 'warn',
      },
    });
    ref.afterClosed().subscribe((confirmed) => {
      if (confirmed) {
        this.usersService.DeActiveUser(u.id).subscribe({
          next: () => {
            this.snackbarService.open('User Deactivated', 'OK', { panelClass: ['snack-success'] });
            this.loadUsers();
          },
        });
      }
    });
  }


}
