import { map } from 'rxjs/operators';
import { QuizAnswer } from './../_models/quiz/QuizAnswer';
import { QuizQuestion } from 'src/_models/quiz/QuizQuestion';
import { Quiz } from './../_models/quiz/Quiz';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';


@Injectable({
  providedIn: 'root'
})
export class AccountService {

  constructor(private httpClient: HttpClient) { }

  myQuizzesIds(): Observable<any> {
    return this.httpClient.get<any>(`${environment.apiUrl}/quiz/my`);
  }

  quizById(quizId: number) {
    return this.httpClient.get<Quiz>(`${environment.apiUrl}/quiz/${quizId}`);
  }

  questionById(questionId: number) {
    return this.httpClient.get<QuizQuestion>(`${environment.apiUrl}/quiz/question/${questionId}`).pipe(map(x => {
      x.answers = x.answers.map(qa => new QuizAnswer(qa.id, qa.text, qa.correct))
      return x;
    }));
  }

  createQuiz(): Observable<Quiz> {
    let fd = new FormData();
    fd.append("name", "new quiz");
    return this.httpClient.post<Quiz>(`${environment.apiUrl}/quiz/create`, fd);
  }

  createQuestion(quizId: number): Observable<QuizQuestion> {
    let fd = new FormData();
    fd.append("text", "New question");
    return this.httpClient.post<QuizQuestion>(`${environment.apiUrl}/quiz/create/question/${quizId}`, fd);
  }

  createAnswer(questionId: number): Observable<QuizAnswer> {
    let fd = new FormData();
    fd.append("text", "New answer");
    return this.httpClient.post<QuizAnswer>(`${environment.apiUrl}/quiz/create/answer/${questionId}`, fd);
  }

  updateQuiz(quiz: Quiz) {
    let fd = new FormData();
    fd.append("id", quiz.id.toString());
    fd.append("name", quiz.name);
    fd.append("category", quiz.category);
    fd.append("public", <string><any>quiz.public);
    if (quiz.avatar)
      fd.append("avatar", quiz.avatar);
    return this.httpClient.put(`${environment.apiUrl}/quiz/update`, fd);
  }

  removeQuiz(quiz: Quiz) {
    return this.httpClient.delete(`${environment.apiUrl}/quiz/delete/${quiz.id}`);
  }

  updateQuestion(question: QuizQuestion) {
    let fd = new FormData();
    console.log(question.image);
    fd.append("image", question.image);
    fd.append("text", question.text);
    fd.append("id", question.id.toString());
    fd.append("type", question.type);
    fd.append("answersType", question.answersType);
    return this.httpClient.put(`${environment.apiUrl}/quiz/update/question`, fd);
  }

  updateAnswer(answer: QuizAnswer, questionId: number): Observable<QuizAnswer> {
    let fd = new FormData();
    if (answer.image)
      fd.append("image", answer.image);
    fd.append("text", answer.text);
    fd.append("id", answer.id.toString());
    fd.append("correct", <string><any>answer.correct);
    return this.httpClient.put<QuizAnswer>(`${environment.apiUrl}/quiz/update/answer/${questionId}`, fd).pipe(map(qa => new QuizAnswer(qa.id, qa.text, qa.correct)));
  }

  getQuizzes(skip: number = 0): Observable<Quiz[]> {
    return this.httpClient.get<Quiz[]>(`${environment.apiUrl}/quiz/top`);
  }
}
