import { Answer } from "../Answers/Answer";


export interface PollQuestion {
    id: number;
    content: string;
    answers: Answer[];
}
