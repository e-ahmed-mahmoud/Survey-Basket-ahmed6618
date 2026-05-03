import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule, MatTableDataSource } from '@angular/material/table';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { RolesService } from '../../../core/services/roles.service';
import { RoleItem } from "../../../shared/models/Auth/RoleItem";
import { RoleEditFormComponent } from '../../../shared/components/role-edit-form.component/role-edit-form.component';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-roles-list',
  standalone: true,
  imports: [CommonModule, MatTableModule, MatButtonModule, MatIconModule, MatProgressBarModule],
  templateUrl: './roles-list.component.html',
})
export class RolesListComponent implements OnInit {

  private readonly rolesService = inject(RolesService);
  private readonly snackbarService = inject(MatSnackBar);

  private readonly dialog = inject(MatDialog);

  loading = signal(true);
  dataSource = new MatTableDataSource<RoleItem>([]);
  cols = ['name', 'isDefault', 'actions'];

  ngOnInit(): void {
    this.loadRoles();
  }

  onAddRole(): void {
    const ref = this.dialog.open(RoleEditFormComponent, {
      width: '500px',
      data: {},
    });
    ref.afterClosed().subscribe((saved) => {
      if (saved) this.loadRoles();
    });
  }

  onEditRole(u: any): void {
    const role = u.name;
    console.log(u);
    const ref = this.dialog.open(RoleEditFormComponent, {
      width: '500px',
      data: { role, isEdit: true, permissions: u.permissions, id: u.id },
    });
    ref.afterClosed().subscribe((saved) => {
      if (saved) this.loadRoles();
    });
  }

  loadRoles() {
    this.rolesService.getAll().subscribe({
      next: (roles) => { this.dataSource.data = roles; this.loading.set(false); },
      error: () => this.loading.set(false),
    });
  }
}
