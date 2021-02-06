import { Router } from '@angular/router';
import { AccountService } from './../../_services/account.service';
import { User } from './../../_models/auth/User';
import { AuthenticationService } from 'src/_services/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {
  user: User;
  quizzes: any[];
  constructor(private authenticationService: AuthenticationService, private accountService: AccountService, private router: Router) {
    this.user = authenticationService.currentUserValue;
    this.accountService.myQuizzesIds().subscribe(x => {
      this.quizzes = x;
    });
  }

  ngOnInit(): void { }

  createQuiz() {
    this.accountService.createQuiz().subscribe(x => {
      this.router.navigate([`/edit/quiz/${x.id}`]);
    });
  }

  logout() {
    this.authenticationService.logout();
  }
}
