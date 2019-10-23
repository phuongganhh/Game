import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BoxRightComponent } from './box-right.component';

describe('BoxRightComponent', () => {
  let component: BoxRightComponent;
  let fixture: ComponentFixture<BoxRightComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BoxRightComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BoxRightComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
