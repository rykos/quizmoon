import { Quiz } from './../../_models/quiz/Quiz';
import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-quiz-tile',
  templateUrl: './quiz-tile.component.html',
  styleUrls: ['./quiz-tile.component.css']
})
export class QuizTileComponent implements OnInit {
  @Input()
  quiz: Quiz;
  
  constructor() { }

  ngOnInit(): void {
  }

}
