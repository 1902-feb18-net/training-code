import { MtgService } from "./mtgservice";

class Main {
    constructor() {
    }

    static Main() {
        let mtgService = new MtgService("https://api.scryfall.com/");
        let textField = <HTMLInputElement>document.getElementById('textField');
        let button = <HTMLButtonElement>document.getElementById('button');
        let output = <HTMLParagraphElement>document.getElementById('output');

        button.addEventListener('click', () => {
            mtgService.getByName(textField.value, card => {
                output.textContent = card.name;
            });
        });
    }
}

Main.Main();
