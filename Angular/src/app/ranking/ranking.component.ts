import { NgForOf } from '@angular/common';
import { ChangeDetectorRef, Component, OnDestroy, OnInit } from '@angular/core';
import { RankingService } from './ranking.service';
import { Subscription, catchError, switchMap } from 'rxjs';
import { VotedQuote } from '../votedquote.model';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-ranking',
  standalone: true,
  imports: [
    NgForOf,
    FormsModule
  ],
  templateUrl: './ranking.component.html',
  styleUrl: './ranking.component.scss'
})
export class RankingComponent implements OnInit, OnDestroy {
  public voted_quotes: VotedQuote[] = [];
  public username: string = "steve";
  private subs: Subscription[] = [];
  constructor(private rankingService: RankingService, private changeDetector: ChangeDetectorRef) {}

  ngOnInit(): void {
    this.refresh();
  }

  refresh() {
    this.subs.push(this.rankingService.getRanking(this.username).subscribe(
      (result) => {
        this.voted_quotes = result;
        this.voted_quotes.forEach((votedquote) => {
          votedquote.color = this.randomColor();
        });
        this.changeDetector.markForCheck();
      },
      (error) => {
        console.log(error);
      }
    ));
  }

  like(quoteId: number) {
    this.subs.push(this.rankingService.getVote(this.username, quoteId).pipe(
      switchMap(() => {
        return this.rankingService.updateVote(this.username, quoteId, 1);
      }),
      catchError(() => {
        return this.rankingService.createVote(this.username, quoteId, 1);
      })
    ).subscribe(() => {
      this.updateLocalVote(quoteId, 1);
    }));
  }
  dislike(quoteId: number) {
    this.subs.push(this.rankingService.getVote(this.username, quoteId).pipe(
      switchMap(() => {
        return this.rankingService.updateVote(this.username, quoteId, -1);
      }),
      catchError(() => {
        return this.rankingService.createVote(this.username, quoteId, -1);
      })
    ).subscribe(() => {
      this.updateLocalVote(quoteId, -1);
    }));
  }

  updateLocalVote(quoteId: number, voteValue: number) {
    const votedQuote = this.voted_quotes.find(v => v.quote.id === quoteId);
    if (votedQuote) {
      votedQuote.vote += voteValue - votedQuote.uservote;
      votedQuote.uservote = voteValue;
      this.changeDetector.markForCheck();
    }
  }

  ngOnDestroy(): void {
    this.subs.forEach((sub) => {
      sub.unsubscribe();
    });
  }

  hexToDecimal = (hex: string) => parseInt(hex, 16);
  randomColor(): string {
    let erg, r, g, b;
    do {
      erg = "#";
      for (let i = 0; i < 6; i++) {
        let ran = Math.floor(Math.random() * 16);
        switch (ran) {
          case 10: erg += "a"; break;
          case 11: erg += "b"; break;
          case 12: erg += "c"; break;
          case 13: erg += "d"; break;
          case 14: erg += "e"; break;
          case 15: erg += "f"; break;
          default: erg += ran; break;
        }
      }

      r = erg.substring(1, 3);
      g = erg.substring(3, 5);
      b = erg.substring(5, 7);
    } while ((this.hexToDecimal(r) + this.hexToDecimal(g) + this.hexToDecimal(b)) < 3 * 127);
    return erg;
  }
}
