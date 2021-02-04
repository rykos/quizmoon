import { QuizAnswer } from './QuizAnswer';
export class QuizQuestion {
    id: number;
    quizId?: number;
    text: string;
    image: File;
    answers: QuizAnswer[];
    type: string = "Text";
    answersType: string = "Text";

    // constructor(id: number, text: string, image: File, answers: QuizAnswer[]) {
    //     this.id = id;
    //     this.text = text;
    //     this.answers = answers;
    //     this.image = image;
    // }
    constructor(question: QuizQuestion) {
        this.id = question.id;
        this.quizId = question.quizId;
        this.text = question.text;
        this.answers = question.answers;
    }
}