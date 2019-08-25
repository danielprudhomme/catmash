import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Cat } from 'src/app/models/cat';
import { ApiService } from 'src/app/services/api.service';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-score',
  templateUrl: './score.component.html',
  styleUrls: ['./score.component.scss']
})
export class ScoreComponent implements OnInit {
  displayedColumns: string[] = ['thumbnail', 'rating'];
  cats: Cat[];
  getCats$: Subscription;

  constructor(
    private router: Router,
    private apiService: ApiService) { }

  ngOnInit() {
    this.getCats$ = this.apiService.getCatList().subscribe(cats => {
      this.cats = cats;
    });
  }

  backToVote() {
    this.router.navigate(['/']);
  }
}
