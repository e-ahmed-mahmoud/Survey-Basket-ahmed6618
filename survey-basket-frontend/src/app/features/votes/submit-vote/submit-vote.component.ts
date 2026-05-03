import { Component, inject, signal, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { ReactiveFormsModule, FormBuilder, Validators, FormArray, FormGroup } from '@angular/forms';
import { MatRadioModule } from '@angular/material/radio';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSnackBar } from '@angular/material/snack-bar';
import { VotesService } from '../../../core/services/votes.service';
import { PollQuestion } from '../../../shared/models/Polls/PollQuestion';

@Component({
  selector: 'app-submit-vote',
  standalone: true,
  imports: [
    CommonModule, ReactiveFormsModule,
    MatRadioModule, MatButtonModule, MatIconModule, MatProgressSpinnerModule,
  ],
  templateUrl: "./submit-vote.component.html",
  styleUrl: "./submit-vote.component.scss",
})
export class SubmitVoteComponent implements OnInit {
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);
  private readonly votesService = inject(VotesService);
  private readonly snackBar = inject(MatSnackBar);
  private readonly fb = inject(FormBuilder);

  questions = signal<PollQuestion[]>([]);
  loading = signal(true);
  submitting = signal(false);

  form: FormGroup = this.fb.group({});

  ngOnInit(): void {
    const pollId = Number(this.route.snapshot.paramMap.get('pollId'));
    this.votesService.getPollQuestions(pollId).subscribe({
      next: (questions) => {
        this.questions.set(questions);
        questions.forEach((q) => {
          this.form.addControl('q_' + q.id, this.fb.control(null, Validators.required));
        });
        console.log(questions);
        this.loading.set(false);
      },
      error: () => this.loading.set(false),
    });
  }

  onSubmit(): void {
    if (this.form.invalid) { this.form.markAllAsTouched(); return; }
    this.submitting.set(true);
    const pollId = Number(this.route.snapshot.paramMap.get('pollId'));

    const voteAnswers = this.questions().map((q) => ({
      questionId: q.id,
      answerId: this.form.value['q_' + q.id],
    }));

    this.votesService.submitVote(pollId, { voteAnswers }).subscribe({
      next: () => {
        this.snackBar.open('Vote submitted successfully! Thank you.', 'OK', { panelClass: ['snack-success'] });
        this.router.navigate(['/votes']);
      },
      error: () => this.submitting.set(false),
    });
  }
}
