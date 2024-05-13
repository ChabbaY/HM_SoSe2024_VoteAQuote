import { Component, OnDestroy, OnInit } from '@angular/core';
import { AutorenService } from './autoren.service';
import { Subscription } from 'rxjs';
import { AuthorQuote } from '../authorquote.model';
import { NgForOf } from '@angular/common';

@Component({
  selector: 'app-autoren',
  standalone: true,
  imports: [NgForOf],
  templateUrl: './autoren.component.html',
  styleUrl: './autoren.component.scss'
})
export class AutorenComponent implements OnInit, OnDestroy {
  public author_quotes: AuthorQuote[] = [];
  private subs: Subscription[] = [];
  constructor(private autorenService: AutorenService) {}

  ngOnInit(): void {
    this.subs.push(this.autorenService.getAuthors().subscribe(
      (response) => {
        response.forEach((author) => {
          this.author_quotes.push({
            author: author,
            quotes: [],
            color: this.randomColor()
          })
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
