import { Answer } from "../Answers/Answer";

export interface Question {
    id: number;
    content: string;
    answers: Answer[];
}
