import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeverageInventoryEditComponent } from './beverage-inventory-edit.component';

describe('BeverageInventoryEditComponent', () => {
  let component: BeverageInventoryEditComponent;
  let fixture: ComponentFixture<BeverageInventoryEditComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeverageInventoryEditComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeverageInventoryEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
