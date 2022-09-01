import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditJourneyComponent } from './add-edit-journey.component';

describe('AddEditJourneyComponent', () => {
  let component: AddEditJourneyComponent;
  let fixture: ComponentFixture<AddEditJourneyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditJourneyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditJourneyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
