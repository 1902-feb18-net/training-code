'use strict';

// when we standardized ES5
// we wanted to fix some bad historical behavior
// without breaking backwards compatibility

// hence, strict mode, opt-in with string expression
// 'use strict' as first line in file or function

// x = 5;
// undeclared variables become ReferenceError

// in general, some old bad silent errors
// became thrown errors.

// in JS, before ES6, we had two scopes for variables:
// 1. global scope
// 2. function scope.

let x = 5;

function func() {
    console.log(x);
    console.log(abc);
    // hoisting moves the var decl above this
    // so it's undefined, not referenceerror
    var abc = 'abc';

    // globalvar = 'abcdef'; // can't do with strict mode.

    if (1 == 1) {
        let variable = 45;
    }
    // console.log(variable); // referenceerror with let
}

func();

// console.log(abc); // ReferenceError

// "let" is actually ES6.
// we used to only have "var"
// "let" was added with block scope semantics

// "var" has a behavior called "hoisting"
// where the _declarations_ are effectively
// moved to the top of the function

// ES6 also added "const"
try {
    const y = 15;
    y = 16; // TypeError, can't change const
} catch (error) {
    console.log(error);
}

function throwsSomething(x) {
    throw 'an error occurred';
}

try {
    throwsSomething(3);
} catch (error) {
    console.log(error);
}

// javascript is not compiled, it's interpreted

// if the JS engine wants to, it can do "JIT compilation" of some kind

// we can write ES6, ES2016, ES2018, etc
// and then compile it down to ES5.

for (let item of [1, 2, 3, '4', {}]) {

}
