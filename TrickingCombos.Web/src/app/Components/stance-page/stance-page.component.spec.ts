import { ComponentFixture, TestBed } from '@angular/core/testing';

import { StancePageComponent } from './stance-page.component';

describe('StancePageComponent', () => {
  let component: StancePageComponent;
  let fixture: ComponentFixture<StancePageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [StancePageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(StancePageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
