import { ComponentFixture, TestBed } from '@angular/core/testing';

import { TrickPageComponent } from './trick-page.component';

describe('TrickPageComponent', () => {
  let component: TrickPageComponent;
  let fixture: ComponentFixture<TrickPageComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [TrickPageComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(TrickPageComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
