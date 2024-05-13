import { NgForOf } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, NgForOf],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'Vote a Quote';

  home = { name: "Ranking", target: "ranking" };
  items = [
    { name: "Autoren", target: "autoren" },
    { name: "Zitate", target: "zitate" }
  ];
}
