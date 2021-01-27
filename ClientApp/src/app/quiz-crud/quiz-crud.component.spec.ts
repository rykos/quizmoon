import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizCrudComponent } from './quiz-crud.component';

describe('QuizCrudComponent', () => {
  let component: QuizCrudComponent;
  let fixture: ComponentFixture<QuizCrudComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizCrudComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizCrudComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
