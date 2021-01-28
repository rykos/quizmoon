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

  constructor(private formBuilder: FormBuilder, private httpClient: HttpClient) { }

  ngOnInit(): void {
    this.quizForm = this.formBuilder.group({
      Name: ['Title', Validators.required],
      Category: ['category'],
      Avatar: [null],
      QuizQuestions: this.formBuilder.array([])
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
    // let fd = new FormData();
    // fd.append("Name", this.quizForm.value.Name);
    // fd.append("Category", this.quizForm.value.Category);
    // for (let i = 0; i < this.quizForm.value.QuizQuestions.length; i++) {
    //   fd.append("QuizQuestions", JSON.stringify(this.quizForm.value.QuizQuestions[i]));
    //   fd.append("QuizQuestionsObj", i.toString());
    // }
    // console.log(fd.getAll("QuizQuestions"));
    // this.httpClient.post(`${environment.apiUrl}/quiz/new`, fd).subscribe(x => console.log(x));
    //{ headers: new HttpHeaders({ 'Content-Type': 'application/json' }) }
  }

  fc(files) {
    let file = files[0];
    let reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {

    };
    reader.onerror = function (error) {
      console.log('Error: ', error);
    };
  }
}
