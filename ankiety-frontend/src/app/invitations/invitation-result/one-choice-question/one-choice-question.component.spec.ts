import { ComponentFixture, TestBed } from '@angular/core/testing';

import { OneChoiceQuestionComponent } from './one-choice-question.component';

describe('OneChoiceQuestionComponent', () => {
  let component: OneChoiceQuestionComponent;
  let fixture: ComponentFixture<OneChoiceQuestionComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ OneChoiceQuestionComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(OneChoiceQuestionComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
