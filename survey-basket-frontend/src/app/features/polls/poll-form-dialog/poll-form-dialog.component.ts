import { Component, inject, signal, Inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, Validators } from '@angular/forms';
import { MatDialogModule, MatDialogRef, MAT_DIALOG_DATA, MatDialogActions } from '@angular/material/dialog';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule, provideNativeDateAdapter } from '@angular/material/core';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { PollsService } from '../../../core/services/polls.service';
import { Poll } from '../../../shared/models/Polls/Poll';

interface DialogData {
  poll: Poll | null;
}

@Component({
  selector: 'app-poll-form-dialog',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatDialogModule, MatFormFieldModule, MatInputModule,
    MatButtonModule, MatIconModule, MatDatepickerModule,
    MatNativeDateModule, MatSlideToggleModule, MatProgressSpinnerModule, MatDialogActions
  ],
  providers: [provideNativeDateAdapter()],
  templateUrl: './poll-form-dialog.component.html',
  styleUrls: ['./poll-form-dialog.component.scss'],
})
export class PollFormDialogComponent {
  private readonly fb = inject(FormBuilder);
  private readonly pollsService = inject(PollsService);
  private readonly dialogRef = inject(MatDialogRef<PollFormDialogComponent>);
  private readonly snackBar = inject(MatSnackBar);
  readonly data: DialogData = inject(MAT_DIALOG_DATA);

  loading = signal(false);
  isEdit = !!this.data.poll;
  title = this.isEdit ? 'Edit Poll' : 'New Poll';

  form = this.fb.nonNullable.group({
    title: [this.data.poll?.title ?? '', [Validators.required, Validators.minLength(3)]],
    summary: [this.data.poll?.summary ?? '', [Validators.required, Validators.minLength(5)]],
    isPublished: [this.data.poll?.isPublished ?? false],
    startAt: [this.data.poll?.startAt ? new Date(this.data.poll.startAt) : new Date() as any, Validators.required],
    endAt: [this.data.poll?.endAt ? new Date(this.data.poll.endAt) : null as any, Validators.required],
  });

  onSubmit(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched(); return;
    }
    this.loading.set(true);
    console.log(this.form.value);
    const raw = this.form.getRawValue();
    const payload = {
      title: raw.title,
      summary: raw.summary,
      isPublished: raw.isPublished,
      startAt: new Date(raw.startAt).toISOString().split('T')[0],
      endAt: new Date(raw.endAt).toISOString().split('T')[0],
    };
    console.log(payload);

    const req$: import('rxjs').Observable<unknown> = this.isEdit
      ? this.pollsService.updatePoll(this.data.poll!.id, payload)
      : this.pollsService.createPoll(payload);

    req$.subscribe({
      next: () => {
        this.snackBar.open(
          `Poll ${this.isEdit ? 'updated' : 'created'} successfully!`,
          'OK',
          { panelClass: ['snack-success'] }
        );
        this.dialogRef.close(true);
      },
      error: () => this.loading.set(false),
    });
  }

  onCancel(): void {
    this.dialogRef.close(false);
  }
}
