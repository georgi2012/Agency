import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowJourneyComponent } from './show-journey.component';

describe('ShowJourneyComponent', () => {
  let component: ShowJourneyComponent;
  let fixture: ComponentFixture<ShowJourneyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowJourneyComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowJourneyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
