import { Routes } from '@angular/router';
import { RankingComponent } from './ranking/ranking.component';
import { AutorenComponent } from './autoren/autoren.component';
import { ZitateComponent } from './zitate/zitate.component';

export const routes: Routes = [
  { path: '', redirectTo: 'ranking', pathMatch: 'full'},
  { path: 'ranking', component: RankingComponent},
  { path: 'autoren', component: AutorenComponent},
  { path: 'zitate', component: ZitateComponent},
  { path: '**', redirectTo: 'ranking'}
];
