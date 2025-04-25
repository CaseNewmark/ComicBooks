import { ComponentFixture, TestBed } from '@angular/core/testing';

import { FloorplanEditComponent } from './floorplan-edit.component';

describe('FloorplanEditComponent', () => {
  let component: FloorplanEditComponent;
  let fixture: ComponentFixture<FloorplanEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [FloorplanEditComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(FloorplanEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
