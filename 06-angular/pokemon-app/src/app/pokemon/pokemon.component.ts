import { Component, OnInit } from '@angular/core';
import { Pokemon } from '../models/pokemon';
import { PokemonApiService } from '../pokemon-api.service';
import { PokemonCollection } from '../models/pokemon-collection';

@Component({
  selector: 'app-pokemon',
  templateUrl: './pokemon.component.html',
  styleUrls: ['./pokemon.component.css']
})
export class PokemonComponent implements OnInit {
  pokemon: Pokemon[] = [
    // { name: 'Bulbasaur', height: 0, weight: 0 }
  ];

  searchText = 'bulbasaur';
  heightWeightClass = 'smaller-text';
  heightWeightClasses = ['smaller-text', 'unused-class'];
  noneFoundStyles = {
    backgroundColor: 'lightgray'
  };
  showError = false;
  error = '';

  // put access modifier on ctor parameter, it will
  // be copied into a property for you.
  constructor(private pokeApi: PokemonApiService) { }

  getPokemon() {
    console.log(`getting pokemon for ${this.searchText}`);
    this.showError = false;

    // subscribing to the observable
    if (this.searchText) {
      this.pokeApi.getByName(this.searchText).subscribe((pokemon: Pokemon) => {
        console.log('received response');
        console.log(pokemon);
        this.pokemon = [pokemon];
      }, error => {
        this.showError = true;
        this.error = error;
      });
    } else {
      this.pokeApi.getAll().subscribe((pokemonCollection: PokemonCollection ) => {
        console.log('received response');
        console.log(pokemonCollection);
        this.pokemon = pokemonCollection.results;
      }, error => {
        this.showError = true;
        this.error = error;
      });
    }
  }

  ngOnInit() {
  }

}
