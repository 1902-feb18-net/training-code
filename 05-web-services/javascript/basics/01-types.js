'use strict';

console.log('hello console');

// "undefined" type (with one possible value, undefined)
let x;
x = undefined;

// number
// same type in JS for whole numbers and decimals
// internally it is basically C# double.
// (64-bit IEEE floating-point number.)
x = 0;
x = -1;
x = 1.5;
x = Infinity;
x = -Infinity;
x = 4 / 0; // infinity
x = NaN;
x = 'abc' / 4;
// NaN
x = NaN + 5;

// string
x = 'ab"\'c';
x = "a'bc"; // double quotes or single quotes, same deal

// boolean
x = true;
x = false;
x = (3 == 3);

// object (reference semantics)
// JS has objects that are not necessarily based on classes
// i can make an object with braces
x = {}; // object literal
x = console;
x = {
    "name": "nick",
    age: 26
};
x.newprop = 123;
// console.log(x.doesntexist);
// like "dynamic" in C#

// null type
// one valid value, null
x = null;
// for historical reasons, "typeof null" is "object"
// similar to C#, we use null to indicate the absence of a value

// function
// quite similar to C# delegate/lambda/func/action
// functions are really type object
// even though typeof says different
x = function (x) { return x + 2; }
x = console.log;

// ES6 added a new type called symbol
// used for globally unique identifier

// 7 types
// string, number, boolean
// null, undefined
// object
// (symbol)


// console.log(x);
// console.log(typeof(x));

// "function declaration"
function printName(name) {
    console.log(name);
}
// because js is dynamically typed, we don't
// declare parameter types or a return type.

// instead of "void return"
// it's just undefined return

console.log(printName("nick"));

//another way to do the same thing:
// "function expression"
let printName2 = function (name) {
    console.log(name);
};

// with ES6, we also have "lambda function" /
// "arrow function"
// syntax is just like C# lambda
let printName3 = name => {
    console.log(name);
};

// most familiar C-style control structures
// let condition = false;

// if (condition) {

// } else if (condition) {

// } else {

// }

// let length = 2;
// for (let i = 0; i < length; i++) {

// }

// while (condition) {

// }

// do {

// } while (condition);

// let key = 1;
// let value = 4;
// switch (key) {
//     case value:

//         break;

//     default:
//         break;
// }

// we have operators

3 == 3;
3 != 3;
3 <= 3;
3 && 3;
3 || 3;

x = 5;

// debugger; // breakpoint

x += 1;
x += 1;

function addTwo(a, b)
{
    console.log(a);
    console.log(b);
    return a + b;
}

console.log(addTwo(1, 3));
console.log();

// extra arguments are silently discarded
console.log(addTwo(1, 3, 6));
console.log();

// not provided arguments become undefined
console.log(addTwo(1));
console.log();
