import { QuizAnswer } from './QuizAnswer';
export class QuizQuestion {
    id: number;
    text: string;
    image: File;
    answers: QuizAnswer[];
}