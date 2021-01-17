import { ComponentFixture, TestBed } from '@angular/core/testing';

import { InvitationResultComponent } from './invitation-result.component';

describe('InvitationResultComponent', () => {
  let component: InvitationResultComponent;
  let fixture: ComponentFixture<InvitationResultComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ InvitationResultComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(InvitationResultComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
