import { AccountService } from './../../_services/account.service';
import { environment } from './../../environments/environment';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Quiz } from './../../_models/quiz/Quiz';
import { Component, OnInit } from '@angular/core';
import { QuizQuestion } from 'src/_models/quiz/QuizQuestion';
import { FormGroup, FormBuilder, FormArray, Validators } from '@angular/forms';

@Component({
  selector: 'app-quiz-crud',
  templateUrl: './quiz-crud.component.html',
  styleUrls: ['./quiz-crud.component.css']
})
export class QuizCrudComponent implements OnInit {
  quizForm: FormGroup;
  //quizAnswers: FormArray[];
  quiz: Quiz = new Quiz();

  constructor(private formBuilder: FormBuilder, private accountService: AccountService) { }

  ngOnInit(): void {
    this.quizForm = this.formBuilder.group({
      Name: ['Title', Validators.required],
      Category: ['category'],
      Avatar: [null],
      QuizQuestions: this.formBuilder.array([])
    });
    this.accountService.createQuiz().subscribe(x => {
      this.quiz = <Quiz>x;
    });
  }

  quizQuestions(): FormArray {
    return this.quizForm.get('QuizQuestions') as FormArray;
  }

  newQuizQuestion(): FormGroup {
    return this.formBuilder.group({
      text: 'question text',
      image: null,
      answers: this.formBuilder.array([])
    });
  }

  addQuizQuestion() {
    this.quizQuestions().push(this.newQuizQuestion());
  }

  quizAnswers(questionIndex: number): FormArray {
    return this.quizQuestions().at(questionIndex).get('answers') as FormArray;
  }

  newAnswer(): FormGroup {
    return this.formBuilder.group({
      text: 'answer text',
      image: null
    });
  }

  addAnswer(questionIndex: number) {
    this.quizAnswers(questionIndex).push(this.newAnswer());
  }

  save() {
    console.log(this.quizForm.value);
    let x = JSON.parse(this.quizForm.value);
    console.log(x);
  }
}
