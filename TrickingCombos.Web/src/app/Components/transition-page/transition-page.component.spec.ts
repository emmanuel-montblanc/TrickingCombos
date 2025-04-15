import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TransitionPageComponent } from './transition-page.component';

describe('TransitionPageComponent', () => {
  let component: TransitionPageComponent;
  let fixture: ComponentFixture<TransitionPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TransitionPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TransitionPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
