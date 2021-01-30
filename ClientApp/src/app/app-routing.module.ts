import { EditQuestionComponent } from './edit-question/edit-question.component';
import { EditQuizComponent } from './edit-quiz/edit-quiz.component';
import { ProfileComponent } from './profile/profile.component';
import { RegisterComponent } from './register/register.component';
import { AuthGuard } from './../_helpers/auth.guard';
import { QuizCrudComponent } from './quiz-crud/quiz-crud.component';
import { LoginComponent } from './login/login.component';
import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'profile', component: ProfileComponent, canActivate: [AuthGuard] },
  { path: 'quiz/new', component: QuizCrudComponent, canActivate: [AuthGuard] },
  { path: 'edit/quiz/:id', component: EditQuizComponent, canActivate: [AuthGuard] },
  { path: 'edit/question/:id', component: EditQuestionComponent, canActivate: [AuthGuard] }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
