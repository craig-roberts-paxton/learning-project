import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ModifyDoorUsersComponent } from './modify-door-users.component';

describe('ModifyDoorUsersComponent', () => {
  let component: ModifyDoorUsersComponent;
  let fixture: ComponentFixture<ModifyDoorUsersComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ModifyDoorUsersComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(ModifyDoorUsersComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
