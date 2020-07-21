import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BeverageInventoryListComponent } from './beverage-inventory-list.component';

describe('BeverageInventoryListComponent', () => {
  let component: BeverageInventoryListComponent;
  let fixture: ComponentFixture<BeverageInventoryListComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BeverageInventoryListComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BeverageInventoryListComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
