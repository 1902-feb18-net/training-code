'use strict';

// loose equality vs strict equality
// aka double equals vs triple equals

compare(1, 2);
compare(1, 1);
compare(1, '1');
compare(true, '1');
compare([1], 1);

function compare(a, b) {
    // one this that ES6 added was string interpolation
    // aka template literals
    // backtick strings let you have multiline literals too
    console.log(`${a} == ${b}: ${a == b}`);
    console.log(`${a} === ${b}: ${a === b}`);
    console.log('');
}

// double equals uses type coercion to attempt
//   to "compare value without caring about type"

// triple equals compares both type and value.
let x = NaN;
if (isNaN(x)) {
}

var y = [1, 2, 3, 8, 4, 5, null, 3, 4];
var sum;
for (let i = 0; i < y.length; i++) {
    sum += y[i];
}
console.log(sum);

// functional way to sum the array
console.log(y.reduce((prev, curr) => prev + curr, 0));


// type coercion to booleans we should know.
// because it happens when we put things in an if condition
if (x) {

}
// the rule is, everything is true, except a handful of special values are false.

// we call values that convert to true "truthy", and the rest are "falsy"
// the falsy values are:
// undefined
// null
// 0 (-0)
// NaN
// ''
// false
