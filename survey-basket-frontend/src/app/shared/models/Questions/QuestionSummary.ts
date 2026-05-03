import { AnswerSummary } from "../Answers/AnswerSummary";


export interface QuestionSummary {
    question: string;
    answers: AnswerSummary[];
}
