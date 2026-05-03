import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { PollQuestion } from '../../shared/models/Polls/PollQuestion';
import { VoteRequest } from '../../shared/models/Votes/VoteRequest';

@Injectable({ providedIn: 'root' })
export class VotesService {
  private readonly http = inject(HttpClient);
  private readonly base = (pollId: number) =>
    `${environment.apiUrl}/api/Polls/${pollId}/Votes`;

  getPollQuestions(pollId: number): Observable<PollQuestion[]> {
    return this.http.get<PollQuestion[]>(`${this.base(pollId)}/GetPollQuestions`);
  }

  submitVote(pollId: number, payload: VoteRequest): Observable<void> {
    return this.http.post<void>(`${this.base(pollId)}/SubmitVote`, payload);
  }
}
