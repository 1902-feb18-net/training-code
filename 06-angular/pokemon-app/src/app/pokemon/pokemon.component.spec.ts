import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { PokemonComponent } from './pokemon.component';
import { PokemonApiService } from '../pokemon-api.service';
import { FormsModule } from '@angular/forms';

describe('PokemonComponent', () => {
  let component: PokemonComponent;
  let fixture: ComponentFixture<PokemonComponent>;

  beforeEach(async(() => {
    const spySvc = jasmine.createSpyObj('PokemonApiService', ['getAll']);

    TestBed.configureTestingModule({
      declarations: [PokemonComponent],
      imports: [
        FormsModule
      ],
      providers: [
        { provide: PokemonApiService, useValue: spySvc }
      ]
    })
      .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(PokemonComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
