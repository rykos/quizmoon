import { QuizAnswer } from './QuizAnswer';
export class QuizQuestion {
    id: number;
    text: string;
    image: File;
    answers: QuizAnswer[];

    constructor(text: string, image: File, answers: QuizAnswer[]) {
        this.text = text;
        this.answers = answers;
        this.image = image;
    }
}