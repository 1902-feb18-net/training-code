import { MtgService } from "./mtgservice";

class Main {
    constructor() {
    }

    static Main() {
        console.log("main");
        document.addEventListener('DOMContentLoaded', () => {
            console.log("dom content loaded");
            let mtgService = new MtgService("https://api.scryfall.com");
            let textField = <HTMLInputElement>document.getElementById('textField');
            let button = <HTMLButtonElement>document.getElementById('button');
            let output = <HTMLParagraphElement>document.getElementById('output');

            button.addEventListener('click', () => {
                console.log("clicked button");
                mtgService.getByName(textField.value, card => {
                    output.textContent = card.name;
                });
            });
        });
    }
}

Main.Main();
