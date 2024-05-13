import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Vote } from './vote.model';
import { VotedQuote } from '../votedquote.model';

@Injectable({
  providedIn: 'root'
})
export class RankingService {

  constructor(private http: HttpClient) { }

  getVote(user: string, quoteId: number) {
    return this.http.get<Vote>(`http://localhost:50000/quotes/${quoteId}/votes/current/${user}`);
  }
  createVote(user:string, quoteId: number, voteValue: number) {
    return this.http.post(`http://localhost:50000/quotes/${quoteId}/votes/${user}`, {
      id: 0,
      quoteId: quoteId,
      user: user,
      voteValue: voteValue,
      timestamp: ''
    });
  }
  updateVote(user:string, quoteId: number, voteValue: number) {
    return this.http.put(`http://localhost:50000/quotes/${quoteId}/votes/${user}`, {
      id: 0,
      quoteId: quoteId,
      user: user,
      voteValue: voteValue,
      timestamp: ''
    });
  }

  getRanking() {
    return this.http.get<VotedQuote[]>('http://localhost:50000/ranking');
  }
}
