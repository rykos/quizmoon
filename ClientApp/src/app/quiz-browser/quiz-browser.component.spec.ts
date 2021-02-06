import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizBrowserComponent } from './quiz-browser.component';

describe('QuizBrowserComponent', () => {
  let component: QuizBrowserComponent;
  let fixture: ComponentFixture<QuizBrowserComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizBrowserComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizBrowserComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
