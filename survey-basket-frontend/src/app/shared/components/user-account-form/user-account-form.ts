import { AfterViewInit, Component, inject, input, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { passwordMatchValidator, UserCreateRequest } from '../../models/Account/UserCreateRequest';
import { UsersService } from '../../../core/services/users.service';
import { UserUpdateRequest } from '../../models/Account/UserUpdateRequest';
import { MAT_DIALOG_DATA, MatDialogActions, MatDialogRef } from '@angular/material/dialog';
import { RolesService } from '../../../core/services/roles.service';
import { RoleItem } from '../../models/Auth/RoleItem';
import { map } from 'rxjs';
import { MatSelect, MatOption } from "@angular/material/select";


interface UserDialogData {
  user: UserUpdateRequest | null;
  isEdit: boolean | null;
  id: string
}

@Component({
  selector: 'app-user-account-form',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatFormFieldModule, MatInputModule, MatButtonModule,
    MatIconModule, MatProgressSpinnerModule, MatDialogActions,
    MatSelect,
    MatOption
  ],
  templateUrl: './user-account-form.html',
  styleUrls: ['./user-account-form.scss'],
})
export class UserAccountForm implements AfterViewInit {

  private readonly fb = inject(FormBuilder);
  private readonly userService = inject(UsersService);
  private readonly snackBar = inject(MatSnackBar);
  private readonly dialogRef = inject(MatDialogRef<UserAccountForm>);
  private readonly rolesService = inject(RolesService);

  readonly data: UserDialogData = inject(MAT_DIALOG_DATA);

  user = signal<UserUpdateRequest | null>(this.data.user);
  isEdit = signal<boolean>(this.data.isEdit ?? false);

  roles = signal<string[]>([]);
  loading = signal(false);

  form = this.fb.nonNullable.group(
    {
      firstName: [this.user()?.firstName ?? '', [Validators.required, Validators.minLength(2)]],
      lastName: [this.user()?.lastName ?? '', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phoneNumber: [this.user()?.phoneNumber ?? '', [Validators.required, Validators.pattern("^[0-9]{7,14}$")]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      confirmPassword: ['', Validators.required],
      roles: [this.user()?.roles ?? ["Member"]]
    },
    { validators: passwordMatchValidator }
  );

  ngAfterViewInit(): void {
    if (this.isEdit() && this.user()?.firstName) {
      this.user.set(this.data.user);
      this.form.get("password")?.disable();
      this.form.get("confirmPassword")?.disable();
      this.form.get("email")?.disable();
    }
    this.getRoles();
    console.log(this.user());
    console.log(this.isEdit());
  }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.loading.set(true);
    if (this.isEdit()) {
      const { password, confirmPassword, email, ...payload } = this.form.getRawValue();
      this.userService.update(this.data.id, payload).subscribe({
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
      this.userService.add(this.form.getRawValue()).subscribe({
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
  getRoles() {
    this.rolesService.getAll().pipe(
      map((roles: RoleItem[]) => {
        this.roles.set(roles.map(r => r.name));
      })
    ).subscribe()
  }
}
