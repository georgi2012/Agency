import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddEditVehComponent } from './add-edit-veh.component';

describe('AddEditVehComponent', () => {
  let component: AddEditVehComponent;
  let fixture: ComponentFixture<AddEditVehComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddEditVehComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddEditVehComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
