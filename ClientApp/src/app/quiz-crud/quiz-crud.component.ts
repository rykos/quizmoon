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

  constructor(private formBuilder: FormBuilder) { }

  ngOnInit(): void {
    this.quizForm = this.formBuilder.group({
      title: ['', Validators.required],
      avatar: [''],
      quizQuestions: this.formBuilder.array([])
    });
  }

  quizQuestions(): FormArray {
    return this.quizForm.get('quizQuestions') as FormArray;
  }

  newQuizQuestion(): FormGroup {
    return this.formBuilder.group({
      text: '',
      image: '',
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
      text: '',
      image: ''
    });
  }

  addAnswer(questionIndex: number) {
    this.quizAnswers(questionIndex).push(this.newAnswer());
  }

  save() {
    console.log(this.quizForm);
  }
}
