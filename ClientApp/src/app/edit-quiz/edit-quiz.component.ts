import { AccountService } from './../../_services/account.service';
import { Quiz } from './../../_models/quiz/Quiz';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-edit-quiz',
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css']
})
export class EditQuizComponent implements OnInit {
  quiz: Quiz;
  constructor(private accountService: AccountService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.accountService.quizById(this.route.snapshot.params.id).subscribe(q => {
      q.quizQuestions.sort((a, b) => a.id - b.id);
      this.quiz = q;
      console.log(q);
    });
  }

  newQuestion() {
    console.log("dupa");
    this.accountService.createQuestion(this.quiz.id).subscribe(x => {
      this.quiz.quizQuestions.push(x);
    });
  }

  updateQuiz() {
    this.accountService.updateQuiz(this.quiz).subscribe(x => {
      console.log(x);
    });
  }

}
