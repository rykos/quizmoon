import { ErrorInterceptor } from './../_helpers/error.interceptor';
import { JwtInterceptor } from './../_helpers/jwt.interceptor';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { NavigationComponent } from './navigation/navigation.component';
import { RegisterComponent } from './register/register.component';
import { FormBuilder, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { QuizCrudComponent } from './quiz-crud/quiz-crud.component';
import { ProfileComponent } from './profile/profile.component';
import { EditQuizComponent } from './edit-quiz/edit-quiz.component';
import { EditQuestionComponent } from './edit-question/edit-question.component';
import { QuizBrowserComponent } from './quiz-browser/quiz-browser.component';
import { QuizTileComponent } from './quiz-tile/quiz-tile.component';
import { ImageComponent } from './image/image.component';
import { QuizDetailsComponent } from './quiz-details/quiz-details.component';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    NavigationComponent,
    RegisterComponent,
    QuizCrudComponent,
    ProfileComponent,
    EditQuizComponent,
    EditQuestionComponent,
    QuizBrowserComponent,
    QuizTileComponent,
    ImageComponent,
    QuizDetailsComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: JwtInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
