import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { QuizTileComponent } from './quiz-tile.component';

describe('QuizTileComponent', () => {
  let component: QuizTileComponent;
  let fixture: ComponentFixture<QuizTileComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ QuizTileComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(QuizTileComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
