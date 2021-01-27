import { QuizQuestion } from './QuizQuestion';
export class Quiz {
    id: number;
    name: string;
    category: string;
    avatar: File;
    quizQuestions: QuizQuestion[];

    constructor() {
        this.quizQuestions = [];
    }
}