import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

import { Cat } from '../models/cat';
import { Vote } from '../models/vote';
import { VoteResult } from '../models/vote-result';

@Injectable({
  providedIn: 'root'
})
export class ApiService {

  apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getNextVote(): Observable<Vote> {
    return this.http.get(`${this.apiUrl}/vote`) as Observable<Vote>;
  }

  vote(voteResult: VoteResult): Observable<Vote> {
    return this.http.post(`${this.apiUrl}/vote`, voteResult) as Observable<Vote>;
  }

  getCatList(): Observable<Cat[]> {
    return this.http.get(`${this.apiUrl}/cat`) as Observable<Cat[]>;
  }
}
