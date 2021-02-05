import { environment } from './../../environments/environment';
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
  question: QuizQuestion;
  answers: QuizAnswer[];

  constructor(private accountService: AccountService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    let id = this.route.snapshot.params.id;
    this.accountService.questionById(id).subscribe(question => {
      console.log(question);
      this.question = new QuizQuestion(<QuizQuestion>question);
      this.answers = question.answers;
    });
  }

  selectChange(value, target, type) {
    if (type == "type")
      target.type = value;
    else if (type == "answersType")
      target.answersType = value;
  }

  onNgSubmit() {
    this.accountService.updateQuestion(this.question).subscribe(x => { });
    console.log(this.question);
  }

  onAnswerSubmit(answerId: number) {
    console.log(answerId);
    let answer = this.answers.find(x => x.id == answerId);
    console.log(this.question.id);

    this.accountService.updateAnswer(answer, this.question.id).subscribe(x => {
      console.log(x);
      answer = x;
      this.answers.find(x => x.id == answerId).dirty = false;
    });
  }

  createAnswer() {
    console.log("create answer");
    this.accountService.createAnswer(this.question.id).subscribe(x => {
      this.question.answers.push(x);
    });
  }

  answerChange(eve, answer: QuizAnswer) {
    answer.dirty = true;
  }


  handleFileUpload(files, target: any) {
    target.image = files[0];
    target.dirty = true;
  }

  questionImgLink(questionId: number): string {
    return `${environment.apiUrl}/image/quiz/question/${questionId}`;
  }

  answerImgLink(answerId: number): string {
    return `${environment.apiUrl}/image/quiz/answer/${answerId}`;
  }

  imgError(eve: HTMLElement) {
    eve.hidden = true;
    console.log(eve);
  }

}
