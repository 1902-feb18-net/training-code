'use strict';

// DOM document object model

// HTML is the serialized format of the DOM.

// (function() {
//     let textPara = document.getElementById('text');
//     console.log(textPara);
// })();

// really we way we do it is register event handlers.

// for right when the page starts...

// "on<event>" properties that we can assign functions to.
// whent eh event happens, they will run.
// this is for the "load" event on the window object.
window.onload = () => {
    let textPara = document.getElementById('text');
    console.log(textPara);
    console.log('this runs second');
};

console.log('this runs first');

// we have a better way to register event handlers
// better, because it allows more than one at a time.

window.addEventListener('load', () => {
    let textPara = document.getElementById('text');

    // change the contents of the element
    textPara.innerHTML += '<em>text</em>'
});

// "load" event fires pretty late... after all images and scripts/styles have finished downloading.

// usually we can use this event instead
document.addEventListener('DOMContentLoaded', () => {
    let textPara = document.getElementById('text');
    textPara.innerHTML += 'text 2';
    // this one ran first...
});

document.addEventListener('DOMContentLoaded', () => {
    let textPara = document.getElementById('text');
    let input = document.getElementById('input');

    let button = document.getElementsByTagName('button')[0];

    let count = 0;

    // click event on button
    button.addEventListener('click', () => {
        count++;
        textPara.innerHTML = `text ${count}`;
        if (count === 10 || input.value === 'google') {
            location.href = "http://google.com"
            // that's how we navigate to new pages
            // (i.e. tell the browser to make GET request.)
        }
    });
});
