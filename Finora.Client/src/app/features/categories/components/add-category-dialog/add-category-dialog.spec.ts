import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddCategoryDialogComponent } from './add-category-dialog';

describe('AddCategoryDialog', () => {
  let component: AddCategoryDialogComponent;
  let fixture: ComponentFixture<AddCategoryDialogComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddCategoryDialogComponent],
    }).compileComponents();

    fixture = TestBed.createComponent(AddCategoryDialogComponent);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
