import { Routes } from '@angular/router';
import { RankingComponent } from './ranking/ranking.component';
import { AutorenComponent } from './autoren/autoren.component';
import { ZitateComponent } from './zitate/zitate.component';
import { HomeComponent } from './home/home.component';

export const routes: Routes = [
  { path: '', redirectTo: 'home', pathMatch: 'full'},
  { path: 'home', component: HomeComponent},
  { path: 'ranking', component: RankingComponent},
  { path: 'autoren', component: AutorenComponent},
  { path: 'zitate', component: ZitateComponent},
  { path: '**', redirectTo: 'home'}
];
