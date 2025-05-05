import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrickDialogComponent } from './trick-dialog.component';

describe('TrickDialogComponent', () => {
  let component: TrickDialogComponent;
  let fixture: ComponentFixture<TrickDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrickDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrickDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
