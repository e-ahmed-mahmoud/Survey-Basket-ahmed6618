export interface PollVotesSummary {
    title: string;
    voteResponses: VoteResponse[];
}

export interface VoteResponse {
    voterName: string;
    voteDate: string;
    questionAnswerResponses: QuestionAnswerResponse[];
}

export interface QuestionAnswerResponse {
    questionContent: string;
    selectedAnswer: string;
}
