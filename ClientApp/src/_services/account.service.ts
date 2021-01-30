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
    return this.httpClient.get<QuizQuestion>(`${environment.apiUrl}/quiz/question/${questionId}`);
  }
}
