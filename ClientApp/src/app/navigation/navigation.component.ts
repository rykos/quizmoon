import { User } from './../../_models/auth/User';
import { AuthenticationService } from 'src/_services/authentication.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navigation',
  templateUrl: './navigation.component.html',
  styleUrls: ['./navigation.component.css']
})
export class NavigationComponent implements OnInit {
  user: User;
  constructor(private authenticationService: AuthenticationService) { }

  ngOnInit(): void {
    this.authenticationService.currentUser.subscribe(user => {
      this.user = user;
    });
  }

}
