import { Router } from '@angular/router';
import { AuthenticationService } from 'src/_services/authentication.service';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  loginForm: FormGroup;

  constructor(private formBuilder: FormBuilder, private authenticationService: AuthenticationService, private router: Router) {
    if (this.authenticationService.currentUserValue) {
      router.navigate(["/profile"]);
    }
  }

  ngOnInit(): void {
    this.loginForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  get f() {
    return this.loginForm.controls;
  }

  onSubmit() {
    this.authenticationService.login(this.f.username.value, this.f.password.value).subscribe(u => {
      this.router.navigate(['']);
    });
  }

}
