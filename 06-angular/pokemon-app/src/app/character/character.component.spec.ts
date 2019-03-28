import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { CharacterComponent } from './character.component';
import { CharacterApiService } from '../character-api.service';
import { Character } from '../models/character';
import { of } from 'rxjs';

describe('CharacterComponent', () => {
  let component: CharacterComponent;
  let fixture: ComponentFixture<CharacterComponent>;

  beforeEach(async(() => {
    const spySvc = jasmine.createSpyObj('CharacterApiService', ['getAll']);
    const chars: Character[] = [];

    // mock "getAll" to return a value of
    // Observable of chars (empty array for now)
    spySvc.getAll.and.returnValue(of(chars));

    TestBed.configureTestingModule({
      declarations: [ CharacterComponent ],
      providers: [
        { provide: CharacterApiService, useValue: spySvc }
      ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(CharacterComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
