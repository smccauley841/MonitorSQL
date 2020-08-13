/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { SqlinstancesComponent } from './sqlinstances.component';

describe('SqlinstancseComponent', () => {
  let component: SqlinstancesComponent;
  let fixture: ComponentFixture<SqlinstancesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ SqlinstancesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(SqlinstancesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
