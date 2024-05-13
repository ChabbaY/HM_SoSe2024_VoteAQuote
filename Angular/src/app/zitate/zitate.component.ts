import { NgForOf } from '@angular/common';
import { Component, OnDestroy, OnInit } from '@angular/core';
import { AuthorQuote } from '../authorquote.model';
import { Subscription } from 'rxjs';
import { ZitateService } from './zitate.service';
import { AutorenService } from '../autoren/autoren.service';

@Component({
  selector: 'app-zitate',
  standalone: true,
  imports: [NgForOf],
  templateUrl: './zitate.component.html',
  styleUrl: './zitate.component.scss'
})
export class ZitateComponent implements OnInit, OnDestroy {
  public author_quotes: AuthorQuote[] = [];
  private subs: Subscription[] = [];
  constructor(private autorenService: AutorenService, private zitateService: ZitateService) {}

  ngOnInit(): void {
    this.subs.push(this.autorenService.getAuthors().subscribe(
      (response) => {
        response.forEach((author) => {
          this.subs.push(this.zitateService.getQuotes(author.id).subscribe(
            (response) => {
              this.author_quotes.push({
                author: author,
                quotes: response,
                color: this.randomColor()
              });
            },
            (error) => {
              console.log(error);
            }
          ));
        });
      },
      (error) => {
        console.log(error);
      }
    ));
    
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
