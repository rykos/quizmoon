import { AccountService } from './../../_services/account.service';
import { Quiz } from './../../_models/quiz/Quiz';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-quiz-browser',
  templateUrl: './quiz-browser.component.html',
  styleUrls: ['./quiz-browser.component.css']
})
export class QuizBrowserComponent implements OnInit {
  quizzes: Quiz[];
  constructor(private accountService: AccountService) { }

  ngOnInit(): void {
    this.accountService.getQuizzes().subscribe(q => {
      this.quizzes = q;
    });
  }

}
