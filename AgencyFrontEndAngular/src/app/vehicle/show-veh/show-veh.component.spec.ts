import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowVehComponent } from './show-veh.component';

describe('ShowVehComponent', () => {
  let component: ShowVehComponent;
  let fixture: ComponentFixture<ShowVehComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowVehComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ShowVehComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
