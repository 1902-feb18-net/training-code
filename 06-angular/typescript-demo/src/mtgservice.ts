import { Card } from "./card";

export class MtgService {
    private url: string;

    getByName(name: string, onSuccess: (card: Card) => void): void {
        let fragment = "/cards/named";
        let url = `${this.url}${fragment}?fuzzy=${name}`;
        fetch(url)
            .then(r => r.json())
            .then(onSuccess)
            .catch(console.log);
        console.log("set up fetch");
    }

    constructor(url: string) {
        this.url = url;
    }
}
