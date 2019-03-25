// by default, TS leaves things in regular global scope.
// however by including "export" or "import", we turn this file into a
// TS module, which works quite similar to ES6 module.

export interface Card {
    name: string;
}
