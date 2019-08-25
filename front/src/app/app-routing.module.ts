import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { ScoreComponent } from './components/score/score.component';
import { VoteComponent } from './components/vote/vote.component';

const routes: Routes = [
  { path: '', component: VoteComponent },
  { path: 'score', component: ScoreComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
