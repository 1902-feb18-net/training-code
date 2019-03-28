import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PokemonComponent } from './pokemon/pokemon.component';
import { CharacterComponent } from './character/character.component';

const routes: Routes = [
  { path: 'pokemon', component: PokemonComponent },
  { path: 'char', component: CharacterComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
