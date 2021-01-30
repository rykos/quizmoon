import { QuizAnswer } from './../../_models/quiz/QuizAnswer';
import { QuizQuestion } from 'src/_models/quiz/QuizQuestion';
import { ActivatedRoute } from '@angular/router';
import { AccountService } from './../../_services/account.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-edit-question',
  templateUrl: './edit-question.component.html',
  styleUrls: ['./edit-question.component.css']
})
export class EditQuestionComponent implements OnInit {
  questionType: string = "Text";
  question: QuizQuestion;
  answers: QuizAnswer[];

  constructor(private accountService: AccountService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = this.route.snapshot.params.id;
    this.accountService.questionById(id).subscribe(question => {
      this.question = question;
      this.answers = question.answers;
      console.log(this.question);
    });
  }

  selectChange(x) {
    this.questionType = x;
    console.log(x);
  }

  onNgSubmit() {
    console.log(this.question);
  }

  onAnswerSubmit(questionId: number) {
    console.log(questionId);
  }

}
