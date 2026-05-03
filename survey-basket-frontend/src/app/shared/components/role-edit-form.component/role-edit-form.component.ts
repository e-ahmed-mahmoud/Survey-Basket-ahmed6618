import { AfterViewInit, Component, inject, NgModuleFactory, signal } from '@angular/core';
import { MatFormField, MatError, MatLabel } from "@angular/material/form-field";
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogRef } from "@angular/material/dialog";
import { MatProgressSpinner, MatProgressSpinnerModule } from "@angular/material/progress-spinner";
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { RolesService } from '../../../core/services/roles.service';
import { UserAccountForm } from '../user-account-form/user-account-form';
import { RoleRequest } from '../../models/Auth/RoleRequest';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';

interface UserDialogData {
  role: string,
  isEdit: boolean,
  permissions: [],
  id: string
}

@Component({
  selector: 'app-role-edit-form.component',
  imports: [ReactiveFormsModule, MatFormField, MatError, MatDialogActions, MatProgressSpinner, MatLabel, ReactiveFormsModule,
    MatInputModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule, MatDialogActions],
  templateUrl: './role-edit-form.component.html',
  styleUrl: './role-edit-form.component.scss',
})
export class RoleEditFormComponent implements AfterViewInit {

  private readonly fb = inject(FormBuilder);
  private readonly snackBar = inject(MatSnackBar);
  private readonly dialogRef = inject(MatDialogRef<UserAccountForm>);
  private readonly rolesService = inject(RolesService);

  readonly data: UserDialogData = inject(MAT_DIALOG_DATA);

  role = signal<string | null>(this.data.role);

  isEdit = signal<boolean>(this.data.isEdit ?? false);

  loading = signal(false);

  form = this.fb.nonNullable.group(
    {
      role: [this.data.role ?? '', [Validators.required, Validators.minLength(2)]],
    });

  ngAfterViewInit(): void {
    if (this.isEdit()) {
      this.role.set(this.data.role);
    }
  }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading.set(true);
    if (this.isEdit()) {
      const payload: RoleRequest = { name: this.form.getRawValue().role, isDefault: false, permissions: [] }
      this.rolesService.update(this.data.id, payload).subscribe({
        next: () => {
          this.snackBar.open('Update successful! Please check your email.', 'OK', {
            panelClass: ['snack-success'],
          });
          this.dialogRef.close(true)
        },
        error: () => this.loading.set(false),
      });
    }
    else {
      console.log(this.form.getRawValue());
      const payload: RoleRequest = { name: this.form.getRawValue().role, isDefault: false, permissions: [] }
      this.rolesService.add(payload).subscribe({
        next: () => {
          this.snackBar.open('Add successful! Please check your email.', 'OK', {
            panelClass: ['snack-success'],
          });
          this.dialogRef.close(true)
        },
        error: () => this.loading.set(false),
      });

    }
  }

  onCancel() {
    this.dialogRef.close(true);
  }

}
