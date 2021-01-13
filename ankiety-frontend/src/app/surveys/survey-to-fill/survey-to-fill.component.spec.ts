import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SurveyToFillComponent } from './survey-to-fill.component';

describe('SurveyToFillComponent', () => {
  let component: SurveyToFillComponent;
  let fixture: ComponentFixture<SurveyToFillComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SurveyToFillComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(SurveyToFillComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
