import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subscription } from 'rxjs';
import { Result } from 'src/app/enums/result';
import { Vote } from 'src/app/models/vote';
import { VoteResult } from 'src/app/models/vote-result';
import { ApiService } from 'src/app/services/api.service';

@Component({
  selector: 'app-vote',
  templateUrl: './vote.component.html',
  styleUrls: ['./vote.component.scss']
})
export class VoteComponent implements OnInit {

  getNextVote$: Subscription;
  postVote$: Subscription;
  vote: Vote;

  constructor(
    private router: Router,
    private apiService: ApiService) { }

  ngOnInit() {
    this.getNextVote$ = this.apiService.getNextVote().subscribe(vote => {
      this.vote = vote;
    });
  }

  ngOnDestroy() {
    if (this.getNextVote$) {
      this.getNextVote$.unsubscribe();
    }
    if (this.postVote$) {
      this.postVote$.unsubscribe();
    }
  }

  onCatClicked(catClickedId: string) {
    const voteResult: VoteResult = {
      cat1Id: this.vote.cat1.id,
      cat2Id: this.vote.cat2.id,
      result: this.vote.cat1.id == catClickedId ? Result.Win : Result.Lose
    };

    this.vote = null;
    this.postVote$ = this.apiService.vote(voteResult).subscribe(vote => {
      this.vote = vote;
    });
  }

  seeScore() {
    this.router.navigate(['/score']);
  }
}
