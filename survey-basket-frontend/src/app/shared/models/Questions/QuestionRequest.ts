import { AnswerRequest } from "../Answers/AnswerRequest";


export interface QuestionRequest {
    content: string;
    answers: AnswerRequest[];
}
