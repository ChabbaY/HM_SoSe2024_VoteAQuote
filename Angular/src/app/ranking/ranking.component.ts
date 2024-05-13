import { NgForOf } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { RankingService } from './ranking.service';
import { Subscription } from 'rxjs';
import { VotedQuote } from '../votedquote.model';
import { AutorenService } from '../autoren/autoren.service';
import { ZitateService } from '../zitate/zitate.service';

@Component({
  selector: 'app-ranking',
  standalone: true,
  imports: [NgForOf],
  templateUrl: './ranking.component.html',
  styleUrl: './ranking.component.scss'
})
export class RankingComponent implements OnInit, OnDestroy {
  public voted_quotes: VotedQuote[] = [];
  public username: string = "steve";
  private subs: Subscription[] = [];
  constructor(private autorenService: AutorenService, private zitateService: ZitateService,
    private rankingService: RankingService) {}

  ngOnInit(): void {
    this.refresh();
  }

  refresh() {
    this.subs.push(this.rankingService.getRanking().subscribe(
      (result) => {
        /*result.forEach((votedquote) => {
          votedquote.color = this.randomColor();
        });*/
        this.voted_quotes = result;
        this.voted_quotes.forEach((votedquote) => {
          votedquote.color = this.randomColor();
        })
      },
      (error) => {
        console.log(error);
      }
    ));
  }

  like(quoteId: number) {
    this.subs.push(this.rankingService.getVote(this.username, quoteId).subscribe(
      () => {
        this.subs.push(this.rankingService.updateVote(this.username, quoteId, 1).subscribe());
      },
      () => {
        this.subs.push(this.rankingService.createVote(this.username, quoteId, 1).subscribe());
      }
    ));
    this.voted_quotes.find(v => v.quote.id == quoteId)!.vote = 1;

    this.refresh();
  }
  dislike(quoteId: number) {
    this.subs.push(this.rankingService.getVote(this.username, quoteId).subscribe(
      () => {
        this.subs.push(this.rankingService.updateVote(this.username, quoteId, -1).subscribe());
      },
      () => {
        this.subs.push(this.rankingService.createVote(this.username, quoteId, -1).subscribe());
      }
    ));
    this.voted_quotes.find(v => v.quote.id == quoteId)!.vote = 1;

    this.refresh();
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
