import { QuizAnswer } from './QuizAnswer';
export class QuizQuestion {
    id: number;
    quizId?: number;
    text: string;
    image: File;
    answers: QuizAnswer[];
    type: string = "Text";
    answersType: string = "Text";

    constructor(question: QuizQuestion) {
        this.id = question.id;
        this.quizId = question.quizId;
        this.text = question.text;
        this.answers = question.answers;
        this.type = question.type;
        this.answersType = question.answersType;
    }
}