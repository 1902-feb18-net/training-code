import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { PokemonComponent } from './pokemon/pokemon.component';
import { HttpClientModule } from '@angular/common/http';

// decorators provide metadata that Angular needs
// to define this class as a (ng) module.

@NgModule({
  // every component needs to be declared in one module.
  declarations: [
    AppComponent, // root component
    PokemonComponent
  ],
  // in "imports", we list all the other modules that we want components/directives
  // from.
  imports: [
    BrowserModule,
    FormsModule, // for ngModel
    HttpClientModule,
    AppRoutingModule
    // to access something from some third party npm package
    // (or angular's npm packages!)
    // 1. npm install <package>
    // 2. TS-import it into the module file that needs it
    // 3. put it in the "imports" array (ng-import)
  ],
  providers: [],
  // specifically our root module should have a bootstrap line
  // which points to the root component
  bootstrap: [AppComponent] // root component
})
export class AppModule { }
