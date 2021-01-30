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
  constructor(private authenticationService: AuthenticationService, private accountService: AccountService) {
    this.user = authenticationService.currentUserValue;
    this.accountService.myQuizzesIds().subscribe(x => {
      this.quizzes = x;
    });
  }

  ngOnInit(): void {
  }

  logout() {
    this.authenticationService.logout();
  }
}
