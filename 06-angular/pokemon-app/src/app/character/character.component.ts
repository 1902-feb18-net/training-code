import { Component, OnInit } from '@angular/core';
import { CharacterApiService } from '../character-api.service';
import { Character } from '../models/character';

@Component({
  selector: 'app-character',
  templateUrl: './character.component.html',
  styleUrls: ['./character.component.css']
})
export class CharacterComponent implements OnInit {

  characters: Character[];

  constructor(private api: CharacterApiService) { }

  ngOnInit() {
    this.api.getAll().subscribe(
      chars => this.characters = chars,
      console.log
    );
  }

}
