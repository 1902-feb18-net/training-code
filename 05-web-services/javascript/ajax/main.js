'use strict';

// AJAX (aka Ajax)
// Asynchronous JavaScript And XML
// a set of tools / technique to send HTTP requests from JavaScript and process the results without the browser reloading the page.

// the traditional tool for the job is XMLHttpRequest object.

document.addEventListener('DOMContentLoaded', () => {
    let jokeBtn = document.getElementById('jokeBtn');
    let jokeBtnFetch = document.getElementById('jokeBtnFetch');
    let jokePara = document.getElementById('jokePara');

    jokeBtn.addEventListener('click', () => {
        let xhr = new XMLHttpRequest();

        xhr.addEventListener('readystatechange', () => {
            console.log(`ready state: ${xhr.readyState}`);
            if (xhr.readyState === 4) {
                // response is here
                if (xhr.status >= 200 && xhr.status < 300) {
                    // if successful...
                    let text = xhr.responseText;
                    // the browser gives us two functions
                    // to help with JSON.
                    // parse (deserialize)
                    // stringify (serialize)
                    let obj = JSON.parse(text);
                    console.log(obj);
                    let joke = obj.value.joke;
                    jokePara.textContent = joke;
                    console.log('success');
                } else {
                    jokePara.textContent = (xhr.statusText === undefined) ? 'Error' : xhr.statusText;
                    console.log('failure');
                }
            }
        });

        console.log('added listener');

        xhr.open('get', 'http://api.icndb.com/jokes/random?escape=javascript');

        console.log('set up request');

        xhr.send();

        console.log('sent request');
    });

    jokeBtnFetch.addEventListener('click', () => {
        // a promise is an object which represents
        // some value that we will eventually get or fail to get.
        // so a promise can "resolve" to success or rejection.


        // defaults to 'get'
        fetch('http://api.icndb.com/jokes/random?escape=javascript')
            .then(response => response.json())
            .then(obj => {
                jokePara.textContent = obj.value.joke;
            })
            .catch(error => console.log(error));
    })
});
