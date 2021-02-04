export class QuizAnswer {
    id: number;
    text: string;
    image?: File;
    correct: boolean;
    textType?: boolean;
    dirty: boolean = false;
    type: string = "Text";

    constructor(id: number, text: string, correct: boolean) {
        this.id = id;
        this.text = text;
        this.correct = correct;
        this.image = null;
        this.textType = null;
    }
}