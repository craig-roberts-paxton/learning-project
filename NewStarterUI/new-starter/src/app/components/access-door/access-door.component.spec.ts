import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AccessDoorComponent } from './access-door.component';

describe('AccessDoorComponent', () => {
  let component: AccessDoorComponent;
  let fixture: ComponentFixture<AccessDoorComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AccessDoorComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(AccessDoorComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
