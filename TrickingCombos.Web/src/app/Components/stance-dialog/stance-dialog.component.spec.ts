import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StanceDialogComponent } from './stance-dialog.component';

describe('StanceDialogComponent', () => {
  let component: StanceDialogComponent;
  let fixture: ComponentFixture<StanceDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StanceDialogComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StanceDialogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
