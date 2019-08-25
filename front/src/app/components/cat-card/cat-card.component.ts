import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { Cat } from 'src/app/models/cat';

@Component({
  selector: 'app-cat-card',
  templateUrl: './cat-card.component.html',
  styleUrls: ['./cat-card.component.scss']
})
export class CatCardComponent implements OnInit {
  @Input() cat: Cat;
  @Output() onClicked = new EventEmitter();

  constructor() { }

  ngOnInit() {
  }

  onClick() {
    this.onClicked.emit();
  }
}
