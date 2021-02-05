import { environment } from './../../environments/environment';
import { AccountService } from './../../_services/account.service';
import { Quiz } from './../../_models/quiz/Quiz';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Component({
  selector: 'app-edit-quiz',
  templateUrl: './edit-quiz.component.html',
  styleUrls: ['./edit-quiz.component.css']
})
export class EditQuizComponent implements OnInit {
  quiz: Quiz;
  newAvatarLink: SafeUrl;
  constructor(private accountService: AccountService, private route: ActivatedRoute, private sanitazer: DomSanitizer) { }

  ngOnInit(): void {
    this.accountService.quizById(this.route.snapshot.params.id).subscribe(q => {
      q.quizQuestions.sort((a, b) => a.id - b.id);
      this.quiz = q;
      console.log(q);
    });
  }

  newQuestion() {
    this.accountService.createQuestion(this.quiz.id).subscribe(x => {
      this.quiz.quizQuestions.push(x);
    });
  }

  updateQuiz() {
    this.accountService.updateQuiz(this.quiz).subscribe(x => {

    });
  }

  quizImageLink(): string | SafeUrl {
    if(this.newAvatarLink){
      return this.newAvatarLink;
    }
    return `${environment.apiUrl}/image/quiz/avatar/${this.quiz.id}`;
  }

  handleFiles(files) {
    this.quiz.avatar = files[0];
    this.newAvatarLink = this.sanitazer.bypassSecurityTrustResourceUrl(URL.createObjectURL(files[0]));
  }

}
